namespace FunctionalFeatures;

public class Linq
{
    private readonly IEnumerable<string> _ingredients = new List<string> { "apple", "banana", "apple", "orange" };

    public enum Fruit
    {
        Apple,
        Banana,
        Orange
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Foreach()
    {
        // Count the number of each type of fruit and remove any oranges
        var fruitCounts = new Dictionary<Fruit, int>();

        foreach (var ingredient in _ingredients)
        {
            var isFruit = Enum.TryParse<Fruit>(ingredient, true, out var fruit);

            if (isFruit && fruit != Fruit.Orange)
            {
                var exists = fruitCounts.TryGetValue(fruit, out int count);

                if (exists)
                {
                    fruitCounts[fruit] = count + 1;
                }
                else
                {
                    fruitCounts.Add(fruit, 1);
                }
            }
        }

        var expectedCounts = new Dictionary<Fruit, int>
        {
            { Fruit.Apple, 2 },
            { Fruit.Banana, 1 },
        };

        Assert.That(fruitCounts, Is.EquivalentTo(expectedCounts));
    }

    [Test]
    public void Linqified()
    {
        var fruitCounts = _ingredients
            .Select<string, Fruit?>(x => Enum.TryParse<Fruit>(x, true, out var fruit) ? fruit : null)
            .Where(x => x != null && x != Fruit.Orange)
            .GroupBy(x => x!.Value)
            .ToDictionary(x => x.Key, x => x.Count());

        var expectedCounts = new Dictionary<Fruit, int>
        {
            { Fruit.Apple, 2 },
            { Fruit.Banana, 1 },
        };

        Assert.That(fruitCounts, Is.EquivalentTo(expectedCounts));
    }
}