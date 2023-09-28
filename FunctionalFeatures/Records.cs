namespace FunctionalFeatures;

public class Records
{
    public class Basket
    {
        public Basket(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; set; }

        public CustomerItem[] CustomerItems { get; set; } = Array.Empty<CustomerItem>();

        public decimal CalculateBasketCost()
        {
            decimal cost = 0;
            foreach (var item in CustomerItems)
            {
                cost += item.Item.Price * item.Quantity;
            }
            return cost;
        }
    }

    public class CustomerItem
    {
        public CustomerItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public Item Item { get; set; }
        public int Quantity { get; set; }
    }

    public class Item
    {
        public Item(string id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Class()
    {
        var appleItem = new Item("1", "Apple", 0.54m);
        var bananaItem = new Item("2", "Banana", 0.40m);
        var orangeItem = new Item("3", "Orange", 0.01m);

        var customer1Basket = new Basket("12345");
        customer1Basket.CustomerItems = new CustomerItem[]
        {
            new CustomerItem(appleItem, 2),
            new CustomerItem(bananaItem, 1),
        };

        var customer2Basket = new Basket("10402");
        customer2Basket.CustomerItems = new CustomerItem[]
        {
            new CustomerItem(appleItem, 2),
            new CustomerItem(orangeItem, 50),
        };

        Assert.That(customer1Basket.CustomerItems[0].Item, Is.EqualTo(customer2Basket.CustomerItems[0].Item));

        // Ref comparison
        var expectedCustomerItems = new CustomerItem[]
        {
            new CustomerItem(appleItem, 2),
            new CustomerItem(bananaItem, 1),
        };

        // Assert.That(customer1Basket.CustomerItems, Is.EqualTo(expectedCustomerItems));

        // Behaviour
        var customer1BasketCost = customer1Basket.CalculateBasketCost();
        var customer2BasketCost = customer2Basket.CalculateBasketCost();
    }


    public record BasketR(string CustomerId, CustomerItemR[] CustomerItems);
    public record CustomerItemR(ItemR Item, int Quantity);
    public record ItemR(string Id, string Name, decimal Price);

    [Test]
    public void Record()
    {
        var appleItem = new ItemR("1", "Apple", 0.54m);
        var bananaItem = new ItemR("2", "Banana", 0.40m);
        var orangeItem = new ItemR("3", "Orange", 0.01m);

        var customer1Basket = new BasketR("12345", new CustomerItemR[]
        {
            new CustomerItemR(appleItem, 2),
            new CustomerItemR(bananaItem, 1),
        });

        var customer2Basket = new BasketR("10402", new CustomerItemR[]
        {
            new CustomerItemR(appleItem, 2),
            new CustomerItemR(orangeItem, 50),
        });


        //customer2Basket.CustomerId = "00000";

        Assert.That(customer1Basket.CustomerItems[0].Item, Is.EqualTo(customer2Basket.CustomerItems[0].Item));

        // Ref comparison?
        var expectedCustomerItems = new CustomerItemR[]
        {
            new CustomerItemR(appleItem, 2),
            new CustomerItemR(bananaItem, 1),
        };

        Assert.That(customer1Basket.CustomerItems, Is.EquivalentTo(expectedCustomerItems));

        // Behaviour
        static decimal CalculateBasketCost(BasketR basket)
        {
            return basket.CustomerItems.Sum(x => x.Item.Price * x.Quantity);
        }

        var customer1BasketCost = CalculateBasketCost(customer1Basket);
        var customer2BasketCost = CalculateBasketCost(customer2Basket);
    }
}