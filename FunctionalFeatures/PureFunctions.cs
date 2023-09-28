namespace FunctionalFeatures;

public class PureFunctions
{
    public class Dinner
    {
        public string Host { get; set; } = default!;

        public string Starter { get; set; } = default!;

        public string Main { get; set; } = default!;

        public string Dessert { get; set; } = default!;

        public Rating? Rating { get; set; } = null;
    }

    public enum Rating
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Unpure()
    {
        var dinner = new Dinner
        {
            Host = "Michelangelo",
            Starter = "Dough Balls",
            Main = "Pizza",
            Dessert = "New York Cheesecake"
        };

        DetermineRating(dinner);

        Assert.That(dinner.Rating, Is.EqualTo(Rating.Five));
    }
        
    private static void DetermineRating(Dinner dinner)
    {
        if (dinner.Main.ToLowerInvariant() == "pizza")
        {
            dinner.Rating = Rating.Five;
        }
        else
        {
            dinner.Rating = Rating.One;
        }
    }

    [Test]
    public void Pure()
    {
        var dinner = new Dinner
        {
            Host = "Michelangelo",
            Starter = "Dough Balls",
            Main = "Pizza",
            Dessert = "New York Cheesecake"
        };

        var dinnerWithRating = DetermineRatingPure(dinner);

        Assert.That(dinnerWithRating.Rating, Is.EqualTo(Rating.Five));
    }

    private static Dinner DetermineRatingPure(Dinner dinner)
    {
        return new Dinner
        {
            Host = dinner.Host,
            Starter = dinner.Starter,
            Main = dinner.Main,
            Dessert = dinner.Dessert,
            Rating = dinner.Main.ToLowerInvariant() == "pizza" ? Rating.Five : Rating.One
        };
    }

    public record DinnerR(string Host, string Starter, string Main, string Dessert)
    {
        public Rating Rating {get; set;}
    }

    [Test]
    public void PureRecord()
    {
        var dinner = new DinnerR("Michelangelo", "Dough Balls", "Pizza", "New York Cheesecake");

        var dinnerWithRating = DetermineRatingPureR(dinner);

        Assert.That(dinnerWithRating.Rating, Is.EqualTo(Rating.Five));
    }

    private static DinnerR DetermineRatingPureR(DinnerR dinner)
    {
        return dinner with { Rating = dinner.Main.ToLowerInvariant() == "pizza" ? Rating.Five : Rating.One };
    }
}