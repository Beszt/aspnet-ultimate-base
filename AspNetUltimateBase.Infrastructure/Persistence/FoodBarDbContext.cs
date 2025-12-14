using Microsoft.EntityFrameworkCore;
using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Infrastructure.Persistence;

public class FoodBarDbContext(
    DbContextOptions<FoodBarDbContext> _options)
    : DbContext(_options)
{
    public DbSet<ProductEntity> Products { get; set; } = default!;
    public DbSet<ProductDetailEntity> ProductsDetails { get; set; } = default!;
    public DbSet<UserEntity> Users { get; set; } = default!;
    public DbSet<RoleEntity> Roles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>()
            .HasIndex(c => c.Barcode)
            .IsUnique();

        modelBuilder.Entity<ProductEntity>()
            .HasOne(c => c.ProductDetails)
            .WithOne(c => c.Product);

        modelBuilder.Entity<ProductEntity>()
            .HasOne(c => c.User)
            .WithMany(c => c.Product)
            .HasForeignKey(c => c.CreatedBy);

        modelBuilder.Entity<ProductDetailEntity>()
            .HasOne(c => c.Product)
            .WithOne(c => c.ProductDetails)
            .HasForeignKey<ProductDetailEntity>(c => c.ProductId);

        modelBuilder.Entity<UserEntity>()
            .HasIndex(c => c.Login)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
            .HasOne(c => c.Role)
            .WithMany(c => c.Users)
            .HasForeignKey(c => c.RoleId);

        modelBuilder.Entity<RoleEntity>()
            .HasMany(c => c.Users)
            .WithOne(c => c.Role);
    }
}
