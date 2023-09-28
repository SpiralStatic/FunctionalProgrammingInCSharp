namespace FunctionalFeatures;

public class RecordsOutput
{
    public record Store(int StoreId, string StoreName, Address Address, int StaffNumbers);

    public record Address(string Line1, string Line2, string City, string Region, string PostCode);

    [Test]
    public void RecordOutput()
    {
        var store = new Store(1, "Budgetins", new Address("42 Wallaby Way", "", "Sydeny", "Sydney", "1001"), 3)
            ;
        Console.WriteLine(store);
    }
}