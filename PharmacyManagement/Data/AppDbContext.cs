using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using PharmacyManagement.Models;

namespace PharmacyManagement.Data
{
    public class AppDbContext:IdentityDbContext<Staff>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
              : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=pharmaComp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


            optionsBuilder.ConfigureWarnings(warnings =>
    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        }


        public DbSet<OrderMedicine> OrderMed { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Medicine> Medicines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            
    base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<Order>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Medicine>().HasKey(e => e.Id);
            modelBuilder.Entity<Order>().HasKey(e => e.Id);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Staff)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.StaffId);


            modelBuilder.Entity<Order>()
                .HasOne(e => e.Customer)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.CustomerId);


            // WithOne(e => e.staff).;
            modelBuilder.Entity<OrderMedicine>().HasKey(e => new {e.MedicineId,e.OrderId});



            modelBuilder.Entity<OrderMedicine>()
                .HasOne(om => om.Prescription)
                .WithMany(o => o.OrderMedicines)
                .HasForeignKey(om => om.OrderId);

            modelBuilder.Entity<OrderMedicine>()
                .HasOne(om => om.Medicine)
                .WithMany(m => m.MedicinesOrder)
                .HasForeignKey(om => om.MedicineId);


            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasKey(i => new { i.RoleId, i.UserId });




    modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
            Id = "1", // You can use Guid.NewGuid().ToString() if needed
            Name = "admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Id = "2",
            Name = "user",
            NormalizedName = "USER"
        }
    );


            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(i => new { i.LoginProvider, i.ProviderKey });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(i => new { i.UserId, i.LoginProvider, i.Name });


            // Seed Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { Id = "201", Name = "Alice Johnson", Position = "Pharmacist" },
                new Staff { Id = "202", Name = "Bob Smith", Position = "Technician" },
                new Staff { Id = "203", Name = "Carol Lee", Position = "Manager" }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 101, Name = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890" },
                new Customer { Id = 102, Name = "Jane Roe", Email = "jane@example.com", PhoneNumber = "0987654321" },
                new Customer { Id = 103, Name = "Sam Green", Email = "sam@example.com", PhoneNumber = "1122334455" }
            );

            // Seed Medicines
            modelBuilder.Entity<Medicine>().HasData(
                new Medicine
                {
                    Id = 1,
                    Name = "Paracetamol",
                    Description = "Pain reliever and fever reducer",
                    Manufacturer = "PharmaCorp",
                    BatchNumber = "PC12345",
                    ExpiryDate = new DateTime(2026, 8, 19, 0, 0, 0, DateTimeKind.Utc),
                    Price = 5.99m,
                    StockQuantity = 100,
                    PrescriptionRequired = false
                },
                new Medicine
                {
                    Id = 2,
                    Name = "Amoxicillin",
                    Description = "Antibiotic for bacterial infections",
                    Manufacturer = "HealthMed",
                    BatchNumber = "HM67890",
                    ExpiryDate = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                    Price = 12.50m,
                    StockQuantity = 50,
                    PrescriptionRequired = true
                },
                new Medicine
                {
                    Id = 3,
                    Name = "Ibuprofen",
                    Description = "Anti-inflammatory drug",
                    Manufacturer = "WellnessPharma",
                    BatchNumber = "WP11223",
                    ExpiryDate = new DateTime(2027, 8, 19,0, 0, 0, DateTimeKind.Utc),
                    Price = 8.75m,
                    StockQuantity = 200,
                    PrescriptionRequired = false
                }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 101,
                    StaffId = "201",
                    Date = new DateTime(2025, 8, 17, 0, 0, 0, DateTimeKind.Utc),
                    TotalAmount = 18.49m,
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 102,
                    StaffId = "202",
                    Date = new DateTime(2025, 8, 18, 0, 0, 0, DateTimeKind.Utc),
                    TotalAmount = 12.50m
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 103,
                    StaffId = "203",
                    Date = new DateTime(2025, 8, 19,0, 0, 0, DateTimeKind.Utc),
                    TotalAmount = 14.74m
                }
            );

            // Composite key for OrderMedicine
            modelBuilder.Entity<OrderMedicine>()
                .HasKey(om => new { om.OrderId, om.MedicineId });

            // Seed OrderMedicine
            modelBuilder.Entity<OrderMedicine>().HasData(
                new OrderMedicine { OrderId = 1, MedicineId = 1, Quantity = 2 },
                new OrderMedicine { OrderId = 1, MedicineId = 3, Quantity = 1 },
                new OrderMedicine { OrderId = 2, MedicineId = 2, Quantity = 1 },
                new OrderMedicine { OrderId = 3, MedicineId = 1, Quantity = 1 },
                new OrderMedicine { OrderId = 3, MedicineId = 3, Quantity = 1 }
            );
        }


    }

}
