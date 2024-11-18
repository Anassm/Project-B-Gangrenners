using Newtonsoft.Json;

class ShowtimesAccess : Access<ShowtimeModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/showtimes.json"));
}