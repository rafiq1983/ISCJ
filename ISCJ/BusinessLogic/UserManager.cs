using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.User;
using MA.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace BusinessLogic
{
    public class UserManager
    {
        public User VerifyLogin(VerifyLoginInput input)
        {
            using (Database db = new Database())
            {
                var user = db.Users.Include(x => x.Contact).Include(x=>x.UserTenants)
                    .SingleOrDefault(x => x.Password == input.Password && x.UserName == input.UserName);
                return user;
            }
        }

        public UserSignupOutput SignupUser(CallContext context, NewUserSignupInput input)
        {
            using (Database db = new Database())
            {
                var user = db.Users.SingleOrDefault(x => x.UserName == input.Email);

                if (user != null)
                {
                    return new UserSignupOutput()
                    {
                        Success = false,
                        ErrorMessage = "User with email " + input.Email + " already exists"
                    };
                }

                user = new User();
                user.UserId = Guid.NewGuid();
                user.Password = input.Password;
                user.UserName = input.Email;
                user.IsEncrypted = false;
                user.RequirePasswordChangeAtLogin = false;
                user.CreateDate = DateTime.UtcNow;
                user.CreateUser = Guid.Empty.ToString();
                user.LastLoginIP = context.CallerIp;

                user.Contact = new Contact()
                {
                    ContactType = 4,
                    CreatedBy = Guid.Empty.ToString(),
                    CreatedDate = DateTime.UtcNow,
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    City = string.Empty,
                    StreetAddress = string.Empty,
                    ZipCode = string.Empty,
                    IsParent = false,
                    State = string.Empty,
                    

                };

                db.Users.Add(user);
                db.SaveChanges();

                return new UserSignupOutput()
                {
                    Success = true
                };
            }
        }

    

    public List<User> GetUsersByName(string name)
        {
            using (Database db = new Database())
            {
                return db.Users.Include(x => x.Contact)
                    .Where(x => (x.Contact.FirstName + " " + x.Contact.LastName).Contains(name))
                    .ToList();
            }
        }

        public User GetUserById(Guid id)
        {
            using (Database db = new Database())
            {
                return db.Users.Include(x => x.Contact).SingleOrDefault(x => x.UserId == id);

            }
        }

        public AddUserToTenantOutput AddUserToTenant(CallContext callContext, AddUserToTenantInput input)
        {
            using (Database db = new Database())
            {
               var user = db.Users.Include(x => x.Contact).SingleOrDefault(x => x.UserId == input.UserId);

               if (user != null)
               {
                   db.UserTenants.Add(new UserTenant()
                   {
                        UserId = user.UserId,
                        TenantId = input.TenantId,
                        RoleCd = input.RoleCd,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = callContext.UserId
                        
                   });

                   db.SaveChanges();

                   return new AddUserToTenantOutput()
                   {
                       Success = true
                   };
               }
               else
               {
                   return new AddUserToTenantOutput()
                   {
                       Success = false
                   };
               }
            }
        }
    }
}
