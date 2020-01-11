using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using MA.Common;
using MA.Common.Entities;
using MA.Common.Entities.Tenants;
using MA.Common.Entities.User;
using MA.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BusinessLogic
{
    public class SignupManager
    {
        private ILogger<SignupManager> _logger;
        private IConfiguration _configuration;

        public SignupManager(ILogger<SignupManager> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }

        public List<Tenant> GetUserTenants()
        {
            using (Database db = new Database())
            {
                return db.Tenants.ToList();
            }

        }

        public bool AddUserToOrganization(CallContext callContext, string userEmail, string role)
        {
            using (Database db = new Database())
            {
                var user = db.Users.SingleOrDefault(x => x.UserName == userEmail);

                if (user != null)
                {
                    var userTenant = db.UserTenants.SingleOrDefault(x => x.TenantId == callContext.TenantId && x.UserId == user.UserId);
                    if (userTenant == null)
                    {
                        userTenant = new UserTenant()
                        {
                            CreateUser = callContext.UserLoginName,
                            CreateDate = DateTime.UtcNow,
                            RoleCd = role,
                            TenantId = callContext.TenantId.Value,
                            UserId = user.UserId

                        };
                        db.UserTenants.Add(userTenant);
                    }
                    else
                    {
                        userTenant = new UserTenant()
                        {
                           ModifiedUser = callContext.UserLoginName,
                           ModifiedDate  = DateTime.UtcNow,
                           RoleCd = role,
                            UserId = user.UserId

                        };
                        db.UserTenants.Add(userTenant);
                        db.Entry(userTenant).State = EntityState.Modified;
                    }

           
                    UserManager userMgr = new UserManager();
                    //TODO: Add these two calls in same transaction.  @Rafiq.
                    userMgr.AddUserNotification(callContext, new AddUserNotificationInput()
                    {
                        UserId = user.UserId,
                        NotificationData = "You have been added to " + db.Tenants.Single(x => x.TenantId == userTenant.TenantId).OrganizationName,
                        NotificationType = "AddedToOrganization"
                    });

                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public OrganizationSignupOutput Signup(OrganizationSignupInput input)
        {
            UserManager mgr = new UserManager();
            var user = mgr.VerifyLogin(new VerifyLoginInput()
            {
                UserName = input.SignupEmail,
                Password = input.Password
            });

            if (user == null)
            {
                return new OrganizationSignupOutput()
                {
                    Success = false,
                    FailureReason = SignupFailureReason.LoginFailed
                };
            }

            using (Database db = new Database())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Tenant tenant =
                            db.Tenants.SingleOrDefault(x => x.OrganizationName == input.OrganizationName);

                        if (tenant != null)
                        {
                            return new OrganizationSignupOutput()
                            {
                                Success = false,
                                FailureReason = SignupFailureReason.OrganizationAlreadyExists
                            };
                        }

                        tenant = new Tenant();
                        tenant.OrganizationName = input.OrganizationName;
                        tenant.TenantId = Guid.NewGuid();
                        tenant.OwnerId = user.UserId;
                        tenant.RowVersion = null;
                        tenant.CreateDate = DateTime.UtcNow;
                        tenant.CreateUser = user.UserName;
                        tenant.OwnerId = user.UserId;
                        tenant.DisplayTimeZone = input.DisplayTimeZone;

                        db.UserTenants.Add(new UserTenant()
                        {
                            UserId = user.UserId,
                            CreateUser = user.UserName,
                            CreateDate = DateTime.UtcNow,
                            TenantId = tenant.TenantId,
                            RoleCd = "ADMIN",
                            Tenant = tenant

                        });

                        db.SequenceCounters.Add(new SequenceCounter()
                        {
                            CounterType = "Tenant",
                            CounterValue = 0,
                            TenantId = tenant.TenantId,
                            CounterName = "StudentCounter"

                        });

                        db.SequenceCounters.Add(new SequenceCounter()
                        {
                            CounterType = "Tenant",
                            CounterValue = 0,
                            TenantId = tenant.TenantId,
                            CounterName = "RegistrationApplicationCounter"

                        });

                        //TODO: Updating User's COntact id causes a dupicate key error in UserTenants for some reason.  Need to debug.
                        /*
                        user.Contact.TenantId = tenant.TenantId;

                        db.Users.Add(user);

                        db.Entry(user).State = EntityState.Modified;
                        db.Entry(user.Contact).State = EntityState.Modified;
                        */
                        db.SaveChanges();
                        SendTenantRegistrationEmail(input);
                        transaction.Commit();
                        return new OrganizationSignupOutput()
                        {
                            Success = true
                        };
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error during tenant signup. " + ex.ToString());
                        transaction.Rollback();
                        return new OrganizationSignupOutput()
                        {
                            Success = false,
                            FailureReason = SignupFailureReason.Other
                        };
                    }

                }
            }
        }


        public static Tenant GetTenant(Guid tenantId)
        {
            using (var db = new Database())
            {
               return
                    db.Tenants.SingleOrDefault(x => x.TenantId == tenantId);
            }

          


        }
        private void SendTenantRegistrationEmail(OrganizationSignupInput input)
        {
            string apiKey = _configuration["SendGridApiKey"];

            SendGridClient client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@icsjsundayschool.com", "Iftikhar Ali");
            var subject = "New Organization setup";
            var to = new EmailAddress(input.SignupEmail, "");
            var plainTextContent = input.OrganizationName;
            var htmlContent = "<strong>Your organization has been setup.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).Result;

            _logger.LogInformation("Email Response Status Code " + response.StatusCode);

            if(response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
                throw new Exception("Unable to send email");


        }
    }
}
