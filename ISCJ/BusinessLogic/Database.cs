using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Registration;
using MA.Common;
using MA.Common.Entities.Contacts;

namespace BusinessLogic
{
  class Database:DbContext
  {
    public Database(DbContextOptions<ContactContext> options) : base(options)
    {

    }

    public Database()
    {; }

    public virtual DbSet<Registration> Registrations { get; set; }

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
