using System.Text.Json;
class MoviesAccess : Access<MovieModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/movies.json"));
}