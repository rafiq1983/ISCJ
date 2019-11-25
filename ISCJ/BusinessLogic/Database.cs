using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Registration;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Product;
using MA.Common.Entities.MasjidMembership;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.User;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MA.Common.Entities.Tenants;
using MA.Common.Models.api;
using MA.Common.Entities.Payments;
using MA.Common.Entities.School;

namespace BusinessLogic
{
  public class Database:DbContext
  {
    public Database(DbContextOptions<ContactContext> options) : base(options)
    {
        
    }

    public Database()
    {; }

    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<Teacher> Teachers { get; set; }
    public virtual DbSet<SubjectMapping> SubjectMappings { get; set; }
    public virtual DbSet<RegistrationApplication> RegistrationApplications { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
    public virtual DbSet<CashPayment> CashPayments { get; set; }
    public virtual DbSet<CheckPayment> CheckPayments { get; set; }
    public virtual DbQuery<AllPayment> AllPaymentIds { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserTenant> UserTenants { get; set; }
    public virtual DbSet<Tenant> Tenants { get; set; }
    public virtual DbSet<MasjidMembership> MasjidMembers { get; set; }
    public virtual DbSet<Metric> Metrics { get; set; }
    public virtual DbSet<MetricValue> MetricValues { get; set; }
    public virtual DbSet<TeacherSubjectMapping> TeacherSubjectMappings { get; set; }
    public virtual DbSet<UserLoginHistory> UserLoginHistory { get; set; }
    public virtual DbSet<BillableProduct> BillableProducts { get; set; }

    public virtual DbSet<ProgramDetail> Programs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<CashPayment>(entity =>
            {
                entity.HasKey(x => x.PaymentId);
                entity.ToTable("CashPayment");
                //entity.Property(x => x.PaymentMethod).HasConversion(new EnumToStringConverter<PaymentMethod>());
               // entity.HasDiscriminator(x => x.PaymentMethod).HasValue<CheckPayment>(PaymentMethod.Check).HasValue<Payment>(PaymentMethod.Cash);
            });

            modelBuilder.Entity<SubjectMapping>(entity =>
            {
                entity.HasKey(x => x.SubjectId);
                entity.HasKey(x => x.ProgramId);
                entity.ToTable("SubjectMapping");
                });


            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(x => x.RoomId);
               
            });



            modelBuilder.Query<AllPayment>(x =>
                x.Property(y => y.PaymentMethod).HasConversion(new EnumToStringConverter<PaymentMethod>()));

            modelBuilder.Entity<CheckPayment>(entity =>
            {
                entity.HasKey(x => x.PaymentId);
                entity.ToTable("CheckPayment");
            });

            modelBuilder.Entity<FinancialAccount>(entity =>
            {
            entity.HasKey(x => x.FinancialAccountId);
            entity.ToTable("FinancialAccount");
                entity.Property(x => x.FinancialAccountType).HasConversion(new EnumToStringConverter<FinancialAccountType>());
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(x => x.TeacherId);
            });

            modelBuilder.Entity<RegistrationApplication>(entity =>
            {
               entity.HasKey(x => x.ApplicationId);
                entity.ToTable("RegistrationApplication");
                entity.HasOne(x => x.MotherContactInfo);
                entity.HasOne(x => x.FatherContactInfo);

            });

            modelBuilder.Entity<ProgramDetail>(entity =>
            {
                entity.HasKey(x => x.ProgramId);
            });

            modelBuilder.Entity<Metric>(entity =>
            {
                entity.HasKey(x => x.MetricId);
                entity.ToTable("Metrics");
                entity.Ignore(x => x.MetricValueDefinitionObject);
            });

            modelBuilder.Entity<TeacherSubjectMapping>(entity =>
            {
                entity.HasKey(x => x.RecordId);
                entity.ToTable("TeacherSubjectAssignment");
                entity.HasOne(x => x.Teacher).WithMany().HasForeignKey(x => x.TeacherId);
            });


            modelBuilder.Entity<MetricValue>(entity =>
            {
                entity.HasKey(x => x.MetricValueId);
                entity.ToTable("MetricValues");
            });

            modelBuilder.Entity<UserTenant>(entity =>
            {
                entity.ToTable("UserTenants");
                entity.HasKey(x =>
                new {
                    x.UserId, x.TenantId
                });
                
            });

                modelBuilder.Entity<Enrollment>(entity =>
      {
        entity.ToTable("Enrollments");
        entity.HasKey(e => e.EnrollmentId);
      }).Entity<Contact>(entity =>
      {
        entity.ToTable("Contacts");
        entity.HasKey(e => e.Guid);
      }).Entity<ContactType>(entity => { entity.ToTable("ContactTypes"); entity.HasKey(e => e.ID); }).Entity<Invoice>(entity =>
      {
        entity.ToTable("Invoice");
        entity.HasKey(e => e.InvoiceId);
          entity.Property(x => x.ReferenceType).HasConversion(new EnumToStringConverter<ReferenceType>());
      })
                .Entity<InvoiceType>(entity =>
                {
                    entity.ToTable("InvoiceType");
                    entity.HasKey(e => e.InvoiceTypeId);
                    
                })
      .Entity<InvoiceItem>(entity =>
      {
        entity.ToTable("InvoiceItem");
        entity.HasKey(e => e.ItemId);
      })
       .Entity<BillableProduct>(entity =>
       {
           entity.ToTable("BillableProduct");
           entity.HasKey(e => e.ProductId);
       });

            modelBuilder.Entity<MasjidMembership>(entity =>
            {
               entity.ToTable("MasjidMembership");
                entity.HasKey(e => e.MembershipId);
            });


            modelBuilder.Entity<FinancialAccount>(entity =>
            {
                entity.HasKey(e => e.FinancialAccountId);
            });

            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.HasKey(e => e.RoleCd);
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.HasKey(e => e.SessionId);
            });

            modelBuilder.Entity<UserRoleLink>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleCd});
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(x => x.RowVersion).HasConversion(DataConverters.SqlTimeStampColumnConverter)
                    .IsRowVersion();

                entity.Property(x => x.IsVerified).HasConversion(DataConverters.IntToBoolConverter());
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(x => x.IsAccountLocked).HasConversion(DataConverters.IntToBoolConverter());
                entity.Property(x => x.IsEncrypted).HasConversion(DataConverters.IntToBoolConverter());
                entity.Property(x => x.RequirePasswordChangeAtLogin).HasConversion(DataConverters.IntToBoolConverter());
                entity.Property(x => x.AuthenticationSource).HasConversion(new EnumToStringConverter<AuthenticationSource>());
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(ConnectionString.Value);
                optionsBuilder.UseLoggerFactory(
  DbCommandConsoleLoggerFactory).EnableSensitiveDataLogging(true);
            }
    }

        public static readonly LoggerFactory DbCommandConsoleLoggerFactory
  = new LoggerFactory(new[] {
      new ConsoleLoggerProvider ((category, level) =>
        category == DbLoggerCategory.Database.Command.Name &&
        level == LogLevel.Information, true)
    });

    }

    static class DataConverters
    {

        public static readonly ValueConverter<string, byte[]> SqlTimeStampColumnConverter = new ValueConverter<string, byte[]>(
    v => GetBytes(v),
    v => Convert.ToBase64String(v));

        private static byte[] GetBytes(string v)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(v);
        }
        //note: byte is a EF Provider Type for tinyint data in sql server.
        //Here we are using a built in convert to convert sql server stored value into boolean.
        private static BoolToZeroOneConverter<byte> _intToBoolConvert = new BoolToZeroOneConverter<byte>();

        private static BytesToStringConverter _bytesToStringConverter = new BytesToStringConverter();
        public static ValueConverter IntToBoolConverter()
        {
            return _intToBoolConvert;
        }

        public static ValueConverter BytesToStringConverter()
        {
            return _bytesToStringConverter;
        }
    }
}
