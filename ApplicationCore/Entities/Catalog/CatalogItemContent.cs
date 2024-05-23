namespace ApplicationCore.Entities.Catalog;

public class CatalogItemContent : BaseEntity
{
    public string Name { get; private set; }
    public byte[] Content { get; private set; }
    public int OrderNumber { private get; set; }
    public CatalogItem CatalogItem { get;private set; }
    public int CatalogItemId { get;private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItemContent()
    {
        
    }
    public CatalogItemContent(string name,byte[] content,int orderNumber,CatalogItem catalogItem)
    {
        Name = name;
        Content = content;
        OrderNumber = orderNumber;
        CatalogItem = catalogItem;
    }

}
