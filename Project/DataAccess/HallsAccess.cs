using Newtonsoft.Json;

class HallsAccess : Access<HallModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/halls.json"));
}