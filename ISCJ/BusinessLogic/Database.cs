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

namespace BusinessLogic
{
  public class Database:DbContext
  {
    public Database(DbContextOptions<ContactContext> options) : base(options)
    {
        
    }

    public Database()
    {; }

    public virtual DbSet<Registration> Registrations { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<MasjidMembership> MasjidMembers { get; set; }

        public virtual DbSet<BillableProduct> BillableProducts { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Registration>(entity =>
      {
        entity.ToTable("Registration");
        entity.HasKey(e => e.RegistrationId);
       
      }).Entity<Contact>(entity =>
      {
        entity.ToTable("Contacts");
        entity.HasKey(e => e.Guid);
      }).Entity<Invoice>(entity =>
      {
        entity.ToTable("Invoice");
        entity.HasKey(e => e.InvoiceId);
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

        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(ConnectionString.Value);
      }
    }

  }
}
