namespace AlbumObjectOriented;

public class CsvReader
{
    public async Task<List<Album>> ReadAsync(string filePath)
    {
        var albums = new List<Album>();

        using var streamReader = File.OpenText(filePath);

        await streamReader.ReadLineAsync();

        while (!streamReader.EndOfStream)
        {
            var row = await streamReader.ReadLineAsync();

            if (row == null)
            {
                return albums;
            }

            var properties = row.Split(",");

            var hasCorrectNumberOfColumns = properties.Length == 4;
            if (!hasCorrectNumberOfColumns)
            {
                throw new ArgumentException("Missing column");
            }

            var genresAsStrings = properties[2].Split("|");
            var genres = new List<Genre>();
            foreach (var genreAsString in genresAsStrings)
            {
                var hasValidGenre = Enum.TryParse<Genre>(genreAsString, out var genre);
                if (!hasValidGenre)
                {
                    throw new ArgumentException("Invalid genre");
                }

                genres.Add(genre);
            }

            var hasValidPrice = decimal.TryParse(properties[3], out var price);
            if (!hasValidPrice)
            {
                throw new ArgumentException("Invalid price");
            }

            var newAlbum = new Album
            {
                AlbumName = properties[0],
                BandName = properties[1],
                Genres = genres,
                Price = price
            };

            albums.Add(newAlbum);
        }

        return albums;
    }
}
