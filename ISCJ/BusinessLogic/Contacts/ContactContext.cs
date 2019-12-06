using System;
using System.Configuration;
using MA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MA.Common.Entities.Contacts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace BusinessLogic
{
    public class ContactContext : DbContext
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
            new ConsoleLoggerProvider((_, __) => true, true)
        });

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

      }).Entity<ContactType>(entity => { entity.ToTable("ContactTypes");  entity.HasKey(e => e.ID); });

        }

     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseLoggerFactory(loggerFactory) //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();

                optionsBuilder.UseSqlServer(ConnectionString.Value);
           }
        }

    }
}
