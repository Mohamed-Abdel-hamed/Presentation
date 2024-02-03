using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        protected void OnModelCreate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p=>p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
               .Property(p => p.List_Price)
               .IsRequired()
                .HasDefaultValue(0);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
               .Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);
        }

    }
}
