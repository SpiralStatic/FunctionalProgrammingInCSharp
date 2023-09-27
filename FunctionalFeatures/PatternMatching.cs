namespace FunctionalFeatures;

public class PatternMatching
{
    private static readonly object[] _testCases =
    {
        new object[] { "apple", "chopped apple" },
        new object[] { "banana", "peeled banana" },
        new object[] { "orange", "peeled orange" },
        new object[] { "cucumber", "not a fruit" },
        new object[] { "12456789", "not a fruit" }
    };

    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(_testCases))]
    public void If(string selectedFruit, string expectedResult)
    {
        string processedFruit = "not a fruit";
        if (selectedFruit == "apple")
        {
            processedFruit = Chop(selectedFruit);
        }
        else if (selectedFruit == "banana" | selectedFruit == "orange")
        {
            processedFruit = Peel(selectedFruit);
        }

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(_testCases))]
    public void Switch(string selectedFruit, string expectedResult)
    {
        string processedFruit;
        switch (selectedFruit)
        {
            case "apple":
                processedFruit = Chop(selectedFruit);
                break;
            case "banana":
            case "orange":
                processedFruit = Peel(selectedFruit);
                break;
            default:
                processedFruit = "not a fruit";
                break;
        }

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(_testCases))]
    public void Pattern(string selectedFruit, string expectedResult)
    {
        var processedFruit = selectedFruit switch
        {
            "apple" => Chop(selectedFruit),
            "banana" or "orange" => Peel(selectedFruit),
            _ => "not a fruit"
        };

        Assert.That(processedFruit, Is.EqualTo(expectedResult));
    }

    private static string Chop(string fruit)
    {
        return $"chopped {fruit}";
    }

    private static string Peel(string fruit)
    {
        return $"peeled {fruit}";
    }
}