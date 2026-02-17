using System;
using Microsoft.EntityFrameworkCore;
using lab22.Models;

namespace lab22.Data
{
    public class ShapeDbContext : DbContext
    {
        public DbSet<ShapeRecord> Shapes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shapes.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування таблиці Shapes
            modelBuilder.Entity<ShapeRecord>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ShapeRecord>()
                .Property(s => s.ShapeType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ShapeRecord>()
                .Property(s => s.CalculatedArea)
                .IsRequired();

            modelBuilder.Entity<ShapeRecord>()
                .Property(s => s.CreatedAt)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
