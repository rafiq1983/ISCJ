using System;
using System.Configuration;
using MA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MA.Common.Entities.Contacts;

namespace BusinessLogic
{
    public class ContactContext : DbContext
    {
       public ContactContext(DbContextOptions<ContactContext> options) : base(options) {
       
        }

    public ContactContext()
    {; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      modelBuilder.Entity<Contact>(entity =>
      {
        entity.ToTable("Contacts");
        entity.HasKey(e => e.Guid);

      }).Entity<ContactType>(entity => { entity.ToTable("ContactType");  entity.HasKey(e => e.ID); });

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
