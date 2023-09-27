using AlbumObjectOriented;

const string albumsCsvFilePath = "Albums.csv";

var csvReader = new CsvReader();
var albums = await csvReader.ReadAsync(albumsCsvFilePath);

foreach(var album in albums)
{
    Console.WriteLine(album);
}
