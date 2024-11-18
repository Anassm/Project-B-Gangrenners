using System.Text.Json;
class ReservationsAccess : Access<ReservationModel>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));
}