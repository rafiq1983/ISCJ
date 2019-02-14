using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Registration;
using MA.Common;

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
