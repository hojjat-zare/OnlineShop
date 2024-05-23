using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.Catalog;

public class PhysicalCatalogItem : CatalogItem , IAggregateRoot
{
    public uint? StockCount { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private PhysicalCatalogItem(){}

    public PhysicalCatalogItem(int catalogTypeId, int catalogBrandId, string description, string name, decimal price, string pictureUri) : base(catalogTypeId, catalogBrandId, description, name, price, pictureUri)
    {
    }

    public override void UpdateDetails(CatalogItemDetails details){
        base.UpdateDetails(details);
        StockCount = details.StockCount;
    }
}
