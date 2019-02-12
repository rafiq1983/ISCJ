using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ISCJ
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
  /*

  public class InvoiceContext : DbContext
  {
    public virtual DbSet<MA.Common.Entities.Invoices.Invoice> Invoices { get; set; }

    public virtual DbSet<MA.Common.Entities.Invoices.InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MA.Common.Entities.Invoices.Invoice>(entity =>
      {
        entity.ToTable("Invoice");
        entity.HasKey(e => e.InvoiceId);

      }).Entity<MA.Common.Entities.Invoices.InvoiceItem>(entity => { entity.ToTable("InvoiceLineItems"); entity.HasKey(e => e.ItemId); });

    }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(ConnectionString.Value);
      }
    }



  }
  */
/*
  public class ContactContext : DbContext
  {
    public ContactContext(DbContextOptions<ContactContext> options) : base(options)
    {

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

      }).Entity<ContactType>(entity => { entity.ToTable("ContactTypes"); entity.HasKey(e => e.ContactTypeId); });

    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(ConnectionString.Value);
      }
    }

  }*/
}
