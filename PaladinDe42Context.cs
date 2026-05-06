using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DE42WPF;

public partial class PaladinDe42Context : DbContext
{
    public PaladinDe42Context()
    {
    }

    public PaladinDe42Context(DbContextOptions<PaladinDe42Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacture> Manufactures { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<PickPoint> PickPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.101.103.35; Trust Server Certificate = true; User=ISP42DE; Password=ISP42; Initial Catalog=PaladinDE42;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.ToTable("Manufacture");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NameManufacture).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateDelivery).HasColumnType("datetime");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Address)
                .HasConstraintName("FK_Order_PickPoint");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Client)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderProduct");

            entity.HasOne(d => d.NumberOrderNavigation).WithMany()
                .HasForeignKey(d => d.NumberOrder)
                .HasConstraintName("FK_OrderProduct_Order");

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.Product)
                .HasConstraintName("FK_OrderProduct_Product");
        });

        modelBuilder.Entity<PickPoint>(entity =>
        {
            entity.ToTable("PickPoint");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Street).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Article).HasMaxLength(255);
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Photo).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UnitMetric).HasMaxLength(255);

            entity.HasOne(d => d.ManufactureNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacture)
                .HasConstraintName("FK_Product_Manufacture");

            entity.HasOne(d => d.SupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplier)
                .HasConstraintName("FK_Product_Supplier");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NameSupplier).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK_User_UserRole");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
