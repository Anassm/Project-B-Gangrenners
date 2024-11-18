using System.Text.Json;

class MovieArchiveAccess : Access<MovieModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/moviearchive.json"));
}