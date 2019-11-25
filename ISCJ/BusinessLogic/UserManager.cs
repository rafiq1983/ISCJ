using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.User;
using MA.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;

namespace BusinessLogic
{
    public class UserManager
    {
        public User VerifyLogin(VerifyLoginInput input)
        {
            using (Database db = new Database())
            {
                PasswordHasher hasher = new PasswordHasher();
              
                var user = db.Users.Include(x => x.Contact).Include(x=>x.UserTenants)
                    .SingleOrDefault(x =>x.UserName == input.UserName);

                if (user == null)
                    return null;

                 var results = hasher.Check(user.Password, input.Password);

                 if (results.Item1 == true)
                     return user;

                 return null;
            }
        }

        public void AddUserLoginAudit(CallContext callContext)
        {
            using (Database db = new Database())
            {
               db.UserLoginHistory.Add(new UserLoginHistory()
                {
                    SessionId = Guid.NewGuid(),
                     LoginDate = DateTime.UtcNow,
                    LoginIP = callContext.CallerIp,
                     DeviceType = callContext.DeviceType,
                    TenantId = callContext.TenantId,
                    UserId = callContext.UserId
                });

              db.SaveChanges();//TODO: Fails for some reason.
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

                PasswordHasher hasher = new PasswordHasher();

                user = new User();
                user.UserId = Guid.NewGuid();
                user.Password = hasher.Hash(input.Password);
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

    public User GetUsersByUserName(string name)
    {
        using (Database db = new Database())
        {
            return db.Users.Include(x => x.Contact).Include(x=>x.UserTenants).SingleOrDefault(x => x.UserName == name);

        }
    }

        public User GetUserById(Guid id)
        {
            using (Database db = new Database())
            {
                return db.Users.Include(x => x.Contact).Include(x=>x.UserTenants).SingleOrDefault(x => x.UserId == id);

            }
        }


        public List<User> GetTenantUsers(CallContext callContext)
        {
            using (Database db = new Database())
            {
                var userTenants = db.UserTenants.Where(x => x.TenantId == callContext.TenantId).Select(x => x.UserId);

                var results = 
                    db.Users.Include(x => x.Contact).Where(x => userTenants.Contains(x.UserId)).ToList();

                foreach (var result in results)
                {
                    result.Password = "";
                }

                return results.ToList();
            }
        }

        public List<UserTenant> GetUserTenants(Guid id)
        {
            using (Database db = new Database())
            {
                var results = db.UserTenants.Include(x => x.Tenant).Where(x => x.UserId == id).ToList();
                return results;
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

    public class HashingOptions
    {
        public int Iterations { get; set; }
    }
    public sealed class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public PasswordHasher(IOptions<HashingOptions> options)
        {
            Options = options.Value;
        }

        public PasswordHasher()
        {
            Options = new HashingOptions() {Iterations = 1000};
        }

        private HashingOptions Options { get; }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Options.Iterations}.{salt}.{key}";
            }
        }

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            var parts = hash.Split('.');

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                                          "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != Options.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }
    }
}
