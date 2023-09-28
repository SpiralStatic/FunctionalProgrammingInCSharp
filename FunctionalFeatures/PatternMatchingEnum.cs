namespace FunctionalFeatures;

public class PatternMatchingEnum
{
    public enum Fruit
    {
        Apple,
        Banana,
        Orange
    }

    private static readonly object[] _testCases =
    {
        new object[] { Fruit.Apple, "chopped apple" },
        new object[] { Fruit.Banana, "peeled banana" },
        new object[] { Fruit.Orange, "peeled orange" },
        //new object[] { "cucumber", "not a fruit" },
        //new object[] { "12456789", "not a fruit" }
    };

    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(_testCases))]
    public void If(Fruit selectedFruit, string expectedResult)
    {
        string processedFruit = "not a fruit";
        if (selectedFruit == Fruit.Apple)
        {
            processedFruit = Chop(selectedFruit);
        }
        else if (selectedFruit == Fruit.Banana | selectedFruit == Fruit.Orange)
        {
            processedFruit = Peel(selectedFruit);
        }

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(_testCases))]
    public void Switch(Fruit selectedFruit, string expectedResult)
    {
        string processedFruit;
        switch (selectedFruit)
        {
            case Fruit.Apple:
                processedFruit = Chop(selectedFruit);
                break;
            case Fruit.Banana:
            case Fruit.Orange:
                processedFruit = Peel(selectedFruit);
                break;
            default:
                processedFruit = "not a fruit";
                break;
        }

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(_testCases))]
    public void Pattern(Fruit selectedFruit, string expectedResult)
    {
        var processedFruit = selectedFruit switch
        {
            Fruit.Apple => Chop(selectedFruit),
            Fruit.Banana => Peel(selectedFruit),
            Fruit.Orange => Peel(selectedFruit),
        };

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    private static string Chop(Fruit fruit)
    {
        return $"chopped {fruit.ToString().ToLowerInvariant()}";
    }

    private static string Peel(Fruit fruit)
    {
        return $"peeled {fruit.ToString().ToLowerInvariant()}";
    }
}