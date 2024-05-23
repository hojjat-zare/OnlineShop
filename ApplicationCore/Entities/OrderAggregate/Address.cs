namespace ApplicationCore.Entities.OrderAggregate;

public class OrderAddress
{
    #pragma warning disable CS8618 // Required by Entity Framework
    private OrderAddress() { }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public string CompleteAddress { get; set; } = null!;
    public string Street { get; private set; }= null!;

    public string City { get; private set; }= null!;

    public string State { get; private set; }= null!;

    public string Country { get; private set; }= null!;

    public string ZipCode { get; private set; }= null!;
    public OrderAddress(string street, string city, string state, string country, string zipcode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
        CompleteAddress = $"{Street}-{City}-{State}-{Country}-{ZipCode}";
    }

    public OrderAddress(string street, string city, string state, string country, string zipcode,string completeAddress)
    {
        CompleteAddress = completeAddress;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
    }
}
