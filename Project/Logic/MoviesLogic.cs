using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.InteropServices;

public class MoviesLogic
{
    public static List<MovieModel> _movies { get; set; } = MoviesAccess.LoadAll();

    public MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll() ?? new List<MovieModel>();
    }

    public static MovieModel GetMovieById(int id)
    {
        MovieModel movie = _movies.Find(movie => movie.Id == id);
        return movie;
    }

    public static MovieModel GetMovieByName(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false);

        return movie;
    }

    public static MovieModel GetMovieByName(string name, List<MovieModel> movies)
    {
        MovieModel movie = movies.Find(movie => movie.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false);

        return movie;
    }

    public static int GetCurrentMovieId(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false);

        return movie.Id;
    }

    public static bool CheckIfMovieInMovies(string name)
    {
        MovieModel movie = GetMovieByName(name);

        return movie != null;
    }

    public static bool CheckIfMovieInMovies(int id)
    {
        MovieModel movie = GetMovieById(id);

        return movie != null;
    }

    public static bool CheckIfMovieInMovies(MovieModel movie)
    {
        MovieModel m = GetMovieById(movie.Id);

        return m != null;
    }

    public static void AddMovie(MovieModel movie)
    {
        if (CheckIfMovieInMovies(movie.Name))
        {
            return;
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(movie.Name))
        {
            MoviesArchiveLogic.RemoveMovie(movie);
        }
        _movies.Add(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public static void AddMovie(string name, string genre, int duration, double cost)
    {
        AddMovie(name, genre, duration, "No summary available", cost);
    }

    public static void AddMovie(string name, string genre, int duration, string summary, double cost)
    {
        MovieModel movie = new MovieModel(GetNextId(), name, genre, duration, false, summary, cost, 0);
        AddMovie(movie);
    }

    public static bool IsValidDateFormat(string date)
    {
        string pattern = @"^\d{4}-\d{2}-\d{2}$";
        return Regex.IsMatch(date, pattern);
    }

    public static bool IsValidTimeOnlyFormat(string time)
    {
        string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d(?:\:[0-5]\d(?:\.\d{1,7})?)?$";
        return Regex.IsMatch(time, pattern);
    }

    public static void RemoveMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return;
        }
        unPromoteMovie(movie);
        MoviesArchiveLogic.AddMovie(movie);
        _movies.Remove(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public static void RemoveMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        RemoveMovie(movie);
    }

    public static bool PromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return false;
        }
        movie.Promoted = true;
        MoviesAccess.WriteAll(_movies);
        return true;
    }

    public static bool PromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        return (PromoteMovie(movie));
    }

    public static bool unPromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return false;
        }
        movie.Promoted = false;
        MoviesAccess.WriteAll(_movies);
        return true;
    }

    public static bool unPromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        return (PromoteMovie(movie));
    }

    public static int GetNextId()
    {
        return _movies.Count() + MoviesArchiveLogic.GetCount() + 1;
    }

    public static Dictionary<string, int> CalculateTotalReservationsPerMovie(MovieModel movie)
    {
        Dictionary<string, int> reservations = new()
        {
            { "Regular", 0 },
            { "VIP", 0 },
            { "VIP+", 0 },
        };

        foreach (ShowtimeModel showtime in ShowtimesLogic.GetShowtimesByMovieId(movie.Id))
        {
            foreach (ReservationModel reservation in ReservationsLogic.GetReservationsByShowtimeId(showtime.Id))
            {
                foreach (int seatId in reservation.SeatIds)
                {
                    SeatModel seat = SeatsLogic.GetSeatById(seatId);
                    if (seat.Type == 1)
                    {
                        reservations["Regular"]++;
                    }
                    else if (seat.Type == 2)
                    {
                        reservations["VIP"]++;
                    }
                    else if (seat.Type == 3)
                    {
                        reservations["VIP+"]++;
                    }
                }
            }
        }

        return reservations;
    }

    public static double CalculateTotalRevenueForFilm(MovieModel movie)
    {
        double totalRevenue = 0;

        foreach (ShowtimeModel showtime in ShowtimesLogic.GetShowtimesByMovieId(movie.Id))
        {
            foreach (ReservationModel reservation in ReservationsLogic.GetReservationsByShowtimeId(showtime.Id))
            {
                totalRevenue += reservation.TotalPrice;
            }
        }

        return totalRevenue;
    }

    public static string FinancialStatusForMovies(bool current, bool past)
    {
        string display = "";
        foreach (MovieModel movie in _movies)
        {
            if (current && !past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count == 0)
                {
                    continue;
                }
            }
            else if (!current && past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count != 0)
                {
                    continue;
                }
            }
            display += movie.ToString() + "\n";
            Dictionary<string, int> reservations = CalculateTotalReservationsPerMovie(movie);
            display += $"Regular: {reservations["Regular"]}\n";
            display += $"VIP: {reservations["VIP"]}\n";
            display += $"VIP+: {reservations["VIP+"]}\n";
            display += $"Total Revenue: {CalculateTotalRevenueForFilm(movie)}\n";
            display += $"Total Cost: {movie.Cost}\n";

            double profit = CalculateTotalRevenueForFilm(movie) - movie.Cost;

            string profitDisplay = profit >= 0
                ? $"\u001b[32mTotal Profit: {profit}\u001b[0m\n" // Green for profit
                : $"\u001b[31mTotal Profit: {profit}\u001b[0m\n"; // Red for loss

            display += profitDisplay;
            display += "----------------------------------------------\n";
        }

        return display;
    }


    public static List<MovieModel> GetMovies(DateTime dayToShow)
    {
        List<MovieModel> movies = new List<MovieModel>();
        List<ShowtimeModel> showtimes = ShowtimesLogic.GetShowtimesByDay(dayToShow);
        foreach (ShowtimeModel showtime in showtimes)
        {
            MovieModel movie = GetMovieById(showtime.MoviesId);
            if (movie != null && !movies.Contains(movie))
            {
                movies.Add(movie);
            }
        }
        return movies;
    }

    public static List<MovieModel> GetMovies(DateTime beginDate, DateTime endDate)
    {
        List<MovieModel> movies = new List<MovieModel>();
        List<ShowtimeModel> showtimes = ShowtimesLogic.GetShowtimesByDay(beginDate, endDate);
        foreach (ShowtimeModel showtime in showtimes)
        {
            MovieModel movie = GetMovieById(showtime.MoviesId);
            if (movie != null && !movies.Contains(movie))
            {
                movies.Add(movie);
            }
        }
        return movies;
    }

    public static List<MovieModel> GetMovies(bool current, bool past)
    {
        List<MovieModel> movies = new();

        foreach (MovieModel movie in _movies)
        {
            if (current && !past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count == 0)
                {
                    continue;
                }
            }
            else if (!current && past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count != 0)
                {
                    continue;
                }
            }
            movies.Add(movie);
        }

        return movies;
    }

    public static string DisplayMovies(DateTime dayToShow)
    {
        List<MovieModel> movies = GetMovies(dayToShow);
        string display = "";
        foreach (MovieModel movie in movies)
        {
            display += movie.ToStringOneLine() + "\n";
        }
        return display;
    }

    public static string DisplayMovies(DateTime beginDate, DateTime endDate)
    {
        List<MovieModel> movies = GetMovies(beginDate, endDate);
        string display = "";
        foreach (MovieModel movie in movies)
        {
            display += movie.ToStringOneLine() + "\n";
        }
        return display;
    }
}