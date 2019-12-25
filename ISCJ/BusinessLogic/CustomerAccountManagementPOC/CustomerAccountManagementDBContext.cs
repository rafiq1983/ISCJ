using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessLogic.CustomerAccountManagementPOC.Entities;

namespace Vcc.EM.Application
{
    public class CustomerAccountManagementDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<DBAddress> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<AccountPaymentSetting> AccountPaymentSettings { get; set; }
        public DbSet<AccountEmailRecipient> AccountEmailRecipients { get; set; }
        public DbSet<ContactWithOnlyOneEmail> ContactWithSingleEmail { get; set; }
        public CustomerAccountManagementDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTables(modelBuilder);
            ConfigureRelations(modelBuilder);
        }

        private void ConfigureRelations(ModelBuilder modelBuilder)
        {
            ;
        }

        private void ConfigureTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBAddress>(entity =>
            {
                entity.ToTable("address");
                entity.HasKey(x => x.AddressId);
            });

            modelBuilder.Entity<AccountPaymentSetting>(entity =>
            {
                entity.ToTable("Account_Payment_Setting");
                entity.HasKey(x => x.PaymentSettingId);
                entity.Property(e => e.IsDefault).HasConversion(v => Convert.ToInt32(v), v => Convert.ToBoolean(v));
            });

            modelBuilder.Entity<AccountEmailRecipient>(entity =>
            {
                entity.ToTable("account_email_recipient");
                entity.HasKey(x => x.AccountEmailRecipientId);
                entity.Property(e => e.SendAutoReceiptIndicator).HasConversion(v => Convert.ToInt32(v), v => Convert.ToBoolean(v));
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("email");
                entity.HasKey(x => x.EmailId);
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("phone");
                entity.HasKey(x => x.PhoneId);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");
                entity.HasKey(x => x.ContactId);
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.ToTable("customer_account");
                entity.HasKey(x => x.AccountId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("supplier_customer");
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.SupplierId)
                    .HasConversion(
                        v => v.ToString(),
                        v => Guid.Parse(v));

                entity.Property(e => e.IsDeleted).HasConversion(v => Convert.ToInt32(v), v => Convert.ToBoolean(v));
            });

            modelBuilder.Entity<DBAddress>(entity =>
            {
                entity.ToTable("address");
                entity.HasKey(e => e.AddressId);
            });

            modelBuilder.Entity<AccountAddressLink>(entity =>
            {
                entity.ToTable("account_address");
                entity.HasKey(x => new { x.AccountId, x.AddressId });
            });

            modelBuilder.Entity<AccountAddressLink>(entity =>
            {
                entity.HasOne(pt => pt.CustomerAccount)
                     .WithMany(p => p.AddressesLink)
                     .HasForeignKey(pt => pt.AccountId);
            });

            modelBuilder.Entity<AccountAddressLink>(entity =>
            {
                entity.HasOne(pt => pt.DBAddress)
                 .WithMany(p => p.AccountsLink)
                .HasForeignKey(pt => pt.AddressId);
            });

            modelBuilder.Entity<AccountContactLink>(entity =>
            {
                entity.ToTable("account_contact");
                entity.HasKey(x => new { x.AccountId, x.ContactId });

                entity.HasOne(pt => pt.CustomerAccount)
                    .WithMany(p => p.ContactsLink)
                    .HasForeignKey(pt => pt.AccountId);

            });

            modelBuilder.Entity<ContactPhone>(entity =>
            {
                entity.ToTable("contact_phone");
                entity.HasKey(x => new { x.ContactId, x.PhoneId });

                entity.HasOne(pt => pt.Contact)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(pt => pt.ContactId);
            });

            modelBuilder.Entity<ContactEmail>(entity =>
            {
                entity.ToTable("contact_email");
                entity.HasKey(x => new { x.ContactId, x.EmailId });

                entity.HasOne(pt => pt.Contact)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(pt => pt.ContactId);
            });

            //Contact with only 1 email poc.
            modelBuilder.Entity<ContactWithOnlyOneEmail>(entity =>
            {
                entity.ToTable("contact");
                entity.HasKey(x => x.ContactId);
                entity.HasOne(x => x.Email).WithOne(b => b.ContactWithOnlyOneEmail)
                    .HasForeignKey<EmailSingle>(b => b.ContactId);
            });

            modelBuilder.Entity<EmailSingle>(entity =>
            {
                entity.ToTable("contact_email");
                entity.HasKey(x => new { x.ContactId, x.EmailId });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
                //optionsBuilder.UseMySQL(_configuration["BT_VCC_ENROLLMENT_CONNECTIONSTRING"]);
                
            }
        }
    }


    public class CustomerAccountManagementDBContext2 : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<ContactWithOnlyOneEmail> ContactWithSingleEmail { get; set; }

        public CustomerAccountManagementDBContext2(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTables(modelBuilder);
            ConfigureRelations(modelBuilder);
        }

        private void ConfigureRelations(ModelBuilder modelBuilder)
        {
            ;
        }

        private void ConfigureTables(ModelBuilder modelBuilder)
        {
            //Contact with only 1 email poc.
            modelBuilder.Entity<ContactWithOnlyOneEmail>(entity =>
            {
                entity.ToTable("contact");
                entity.HasKey(x => x.ContactId);
                entity.HasOne(x => x.Email).WithOne(x=>x.ContactWithOnlyOneEmail)
                    .HasForeignKey<EmailSingle>(b => b.ContactId);
            });

            modelBuilder.Entity<EmailSingle>(entity =>
            {
                entity.ToTable("contact_email");
                entity.HasKey(x => new { x.ContactId, x.EmailId });
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("email");
                entity.HasKey(x => x.EmailId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
               // optionsBuilder.UseMySQL(_configuration["BT_VCC_ENROLLMENT_CONNECTIONSTRING"]);

            }
        }
    }
}