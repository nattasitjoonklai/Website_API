using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Website_API.Models;

public partial class PersonalTestContext : DbContext
{
    public PersonalTestContext()
    {
    }

    public PersonalTestContext(DbContextOptions<PersonalTestContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<C> Customers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=DESKTOP-6CJVK7Q\\SQLEXPRESS; Database=PersonalTest; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Customers>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC2740E5753E");

        //    entity.ToTable("CUSTOMERS");

        //    entity.Property(e => e.Id)
        //        .ValueGeneratedNever()
        //        .HasColumnName("ID");
        //    entity.Property(e => e.Address)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .IsFixedLength()
        //        .HasColumnName("ADDRESS");
        //    entity.Property(e => e.Age).HasColumnName("AGE");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(20)
        //        .IsUnicode(false)
        //        .HasColumnName("NAME");
        //    entity.Property(e => e.Salary)
        //        .HasColumnType("decimal(18, 2)")
        //        .HasColumnName("SALARY");
        //});

        modelBuilder.Entity<Personal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Personal");

            entity.Property(e => e.Idcard)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDCARD");
            entity.Property(e => e.Userid).HasColumnName("USERID");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
