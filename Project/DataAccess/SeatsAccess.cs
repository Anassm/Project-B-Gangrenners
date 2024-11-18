using System.Text.Json;

class SeatsAccess : Access<SeatModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/seats.json"));
}