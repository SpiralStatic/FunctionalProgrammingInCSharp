namespace FunctionalPlayground;

public static class PureFunctions
{
    public static void Unpure()
    {
        var ogWord = "unpure";

        var unpure = (string word) =>
        {
            return word + "-" + DateTime.Now.Ticks.ToString();
        };
        
        Console.WriteLine(unpure(ogWord));
        Console.WriteLine(unpure(ogWord));
    }

    public static void Pure()
    {
        var ogWord = "pure";
        var ticks = DateTime.Now.Ticks;

        var pure = (string word, long ticks) =>
        {
            return word + "-" + ticks.ToString();
        };
        
        Console.WriteLine(pure(ogWord, ticks));
        Console.WriteLine(pure(ogWord, ticks));
    }
}
