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

        modelBuilder.Entity<ProductEntity>(builder =>
        {
            builder
                .Property(c => c.Barcode)
                .IsRequired();


            builder
                .HasIndex(c => c.Barcode)
                .IsUnique();

            builder
                .HasOne(c => c.ProductDetails)
                .WithOne(c => c.Product);

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.Product)
                .HasForeignKey(c => c.CreatedBy);
        });

        modelBuilder.Entity<ProductDetailEntity>(builder =>
        {
            builder
                .HasOne(c => c.Product)
                .WithOne(c => c.ProductDetails)
                .HasForeignKey<ProductDetailEntity>(c => c.ProductId);
        });

        modelBuilder.Entity<UserEntity>(builder =>
        {
            builder
                .Property(c => c.Login)
                .IsRequired();

            builder
                .HasIndex(c => c.Login)
                .IsUnique();

            builder
                .Property(c => c.Password)
                .IsRequired();

            builder
                .HasOne(c => c.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.RoleId);
        });

        modelBuilder.Entity<RoleEntity>(builder =>
        {
            builder
                .HasMany(c => c.Users)
                .WithOne(c => c.Role);
        });
    }
}
