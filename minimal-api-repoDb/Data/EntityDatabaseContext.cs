using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.Data;

public partial class EntityDatabaseContext : DbContext
{
    public EntityDatabaseContext()
    {
    }

    public EntityDatabaseContext(DbContextOptions<EntityDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(1024);
            entity.Property(e => e.FirstName).HasMaxLength(1024);
            entity.Property(e => e.LastName).HasMaxLength(1024);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.HasOne(d => d.Employee).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
