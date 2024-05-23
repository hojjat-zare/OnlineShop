using System;
using Ardalis.GuardClauses;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities.Catalog;
using System.Collections.ObjectModel;
using ApplicationCore.Exceptions;

namespace ApplicationCore.Entities.Catalog;

public class CatalogItem : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUri { get; private set; }
    public int CatalogTypeId { get; private set; }
    public CatalogType? CatalogType { get; private set; }
    public int CatalogBrandId { get; private set; }
    public CatalogBrand? CatalogBrand { get; private set; }
    private List<CatalogItemContent> _contents = new List<CatalogItemContent>();
    public ReadOnlyCollection<CatalogItemContent> Contents => _contents.AsReadOnly();

    #pragma warning disable CS8618 // Required by Entity Framework
    protected internal CatalogItem()
    {
        
    }

    public CatalogItem(int catalogTypeId,
        int catalogBrandId,
        string description,
        string name,
        decimal price,
        string pictureUri)
    {
        CatalogTypeId = catalogTypeId;
        CatalogBrandId = catalogBrandId;
        Description = description;
        Name = name;
        Price = price;
        PictureUri = pictureUri;
    }

    public virtual void AddContent(Content content){
        Guard.Against.NullOrEmpty(content.Name,nameof(content.Name));
        Guard.Against.Null(content.TheContent,nameof(content.TheContent));
        if (_contents.Any(c => c.Name == content.Name))
            throw new DuplicateException($"content with name {content.Name} has already exsit in contents");
        _contents.Add(new CatalogItemContent(content.Name,content.TheContent,_contents.Count(),this));
    }

    public virtual void UpdateDetails(CatalogItemDetails details)
    {
        Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
        Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));
        Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));
        
        Name = details.Name;
        Description = details.Description;
        Price = details.Price;
    }

    public void UpdateBrand(int catalogBrandId)
    {
        Guard.Against.Zero(catalogBrandId, nameof(catalogBrandId));
        CatalogBrandId = catalogBrandId;
    }

    public void UpdateType(int catalogTypeId)
    {
        Guard.Against.Zero(catalogTypeId, nameof(catalogTypeId));
        CatalogTypeId = catalogTypeId;
    }

    public void UpdatePictureUri(string pictureName)
    {
        if (string.IsNullOrEmpty(pictureName))
        {
            PictureUri = string.Empty;
            return;
        }
        PictureUri = $"images\\products\\{pictureName}?{new DateTime().Ticks}";
    }

    public readonly record struct Content{
        public Content(string name, byte[] theContent)
        {
            Name = name;
            TheContent = theContent;
        }
        public string Name { get;  }
        public byte[] TheContent { get;  }

    }

    public readonly record struct CatalogItemDetails
    {
        public string? Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
        public uint? StockCount {get;}

        public CatalogItemDetails(string? name, string? description, decimal price,uint stockCount)
        {
            Name = name;
            Description = description;
            Price = price;
            StockCount = stockCount;
        }
    }
}
