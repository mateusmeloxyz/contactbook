using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ContactBook.Domain.Entities;

namespace ContactBook.Persistance
{
    public class ContactBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ContactBookDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(new Contact("Ana", "ana@test.com", "81987654321")
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                IsActive = true
            }); 
            modelBuilder.Entity<Contact>().HasData(new Contact("Beto", "beto@test.com", "81912345678")
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                IsActive = true
            });
        }
    }
}
