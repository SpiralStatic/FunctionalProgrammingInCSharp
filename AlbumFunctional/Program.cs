using AlbumFunctional;
using LaYumba.Functional;

// Do
"Albums.csv"
    .ReadAlbums()
    .ForEachAsync((result, index) =>
        result.Match(
            (error) => Console.WriteLine($"Row: {index + 1} - {error}"),
            (album) => album.LogAlbum()
        )
    )
    .Wait();

namespace AlbumFunctional
{
    public record ParsingColumnError() : Error("Invalid number of columns");
    public record ParsingPriceError() : Error("Invalid Price");
    public record ParsingGenreError() : Error("Invalid Genre");

    public enum Genre { Rock, Pop, Electronic }
    public record Album(string AlbumName, string BandName, List<Genre> Genres, decimal Price);

    public static class AlbumExtensions
    {
        public static void LogAlbum(this Album a)
        {
            Console.WriteLine($"{a.AlbumName} - {a.BandName} - {string.Join("|", a.Genres)} - £{a.Price}");
        }

        public static IAsyncEnumerable<Either<Error, Album>> ReadAlbums(this string filePath)
           => ReadLines(filePath)
               .Select(
                   row => F.Right(row.Split(","))
                           .Bind(ParseAlbum)
               );

        private static async IAsyncEnumerable<string> ReadLines(string filePath)
        {
            using StreamReader reader = File.OpenText(filePath);
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
                yield return await reader.ReadLineAsync();
        }

        private static Either<Error, Album> ParseAlbum(string[] album) =>
            F.Right(album)
                .Bind(ValidateAlbum)
                .Map(x => new Album(x[0], x[1], ParseGenres(x), decimal.Parse(x[3])));

        private static List<Genre> ParseGenres(string[] x)
        {
            return x[2].Split('|').Select(g => System.Enum.Parse<Genre>(g)).ToList();
        }

        private static Either<Error, string[]> ValidateAlbum(string[] album) =>
            F.Right(album)
                .Bind(ValidateColumns)
                .Bind(ValidatePrice)
                .Bind(ValidateGenres);

        private static Either<Error, string[]> ValidateColumns(string[] columns) => columns.Length == 4 ? columns : new ParsingColumnError();
        private static Either<Error, string[]> ValidatePrice(string[] columns) => decimal.TryParse(columns[3], out var _) ? columns : new ParsingPriceError();

        private static Either<Error, string[]> ValidateGenres(string[] columns) =>
            columns[2]
                .Split('|')
                .All(genre => System.Enum.TryParse<Genre>(genre, out var _))
                    ? columns
                    : new ParsingGenreError();    
    }
}