namespace AlbumObjectOriented;

public class Album
{
    public string AlbumName { get; set; } = default!;

    public string BandName { get; set; } = default!;

    public List<Genre> Genres { get; set; } = default!;

    public decimal Price { get; set; } = default!;

    public override string ToString()
    {
        return $"{AlbumName} - {BandName} - {string.Join("|", Genres)} - £{Price}";
    }
}
