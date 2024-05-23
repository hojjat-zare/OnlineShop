using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.Catalog;
using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config;

public static class CatalogContextConfig
{
    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity.Property(e => e.BuyerId).HasMaxLength(256);
        });

        modelBuilder.Entity<BasketItem>(entity =>
        {
            entity.HasIndex(e => e.BasketId, "IX_BasketItems_BasketId");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CatalogItem>(entity =>
        {
            entity.ToTable("Catalog");

            entity.HasIndex(e => e.CatalogBrandId, "IX_Catalog_CatalogBrandId");

            entity.HasIndex(e => e.CatalogTypeId, "IX_Catalog_CatalogTypeId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PictureUri).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.HasMany(e => e.Contents)
            .WithOne(c => c.CatalogItem);

        });

        modelBuilder.Entity<CatalogItemContent>(entity =>
        {
            entity.ToTable("CatalogContent");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.HasIndex(e => e.CatalogItemId,"ID_CatalogContent_CatalogItemId")
            .IsClustered(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });



        modelBuilder.Entity<CatalogBrand>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(100);
        });

        modelBuilder.Entity<CatalogType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.BuyerId).HasMaxLength(256);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity
            .HasOne(o => o.ShipToAddress)
            .WithOne(oa => oa.Order)
            .HasForeignKey<OrderAddress>(oa => oa.OrderId)
            .IsRequired();
        });

        modelBuilder.Entity<OrderAddress>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("OrderAddress");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CompleteAddress).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.State).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(18);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();

                io.Property(cio => cio.ProductName)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            entity.Property(oi => oi.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            });
    }
}
