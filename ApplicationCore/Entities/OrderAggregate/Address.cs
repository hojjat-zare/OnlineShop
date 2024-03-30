namespace ApplicationCore.Entities.OrderAggregate;

public class OrderAddress // ValueObject
{
    #pragma warning disable CS8618 // Required by Entity Framework
    private OrderAddress() { }
    public string CompleteAddress { get; set; }
    public string Street { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }

    public string Country { get; private set; }

    public string ZipCode { get; private set; }
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
