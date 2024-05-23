using System;
using System.Collections.Generic;
// using Infrastructure.Data.Entities;
using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.BuyerAggregate;
using ApplicationCore.Entities.Catalog;
using ApplicationCore.Entities.OrderAggregate;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class CatalogContext : DbContext
{
    public CatalogContext()
    {
    }

    public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Basket> Baskets { get; set; }
    public virtual DbSet<BasketItem> BasketItems { get; set; }
    public virtual DbSet<CatalogItem> CatalogItems { get; set; }
    public virtual DbSet<PhysicalCatalogItem> PhysicalCatalogItems { get; set; }
    public virtual DbSet<CatalogItemContent> CatalogItemContents { get; set; }
    public virtual DbSet<CatalogBrand> CatalogBrands { get; set; }
    public virtual DbSet<CatalogType> CatalogTypes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderAddress> OrderAddresses { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CatalogContextConfig.OnModelCreating(modelBuilder);
    }

}
