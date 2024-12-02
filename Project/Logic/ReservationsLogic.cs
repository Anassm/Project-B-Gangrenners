public class ReservationsLogic
{
    private static Random _random = new Random();
    private static List<ReservationModel> _reservations { get; set; } = ReservationsAccess.LoadAll();

    public ReservationsLogic()
    {
        //_reservations = ReservationsAccess.LoadAll();
    }

    public static List<ReservationModel> OrderReservations()
    {
        List<ShowtimeModel> showtimes = ShowtimesAccess.LoadAll();

        List<ReservationModel> orderedReservations = _reservations
            .Join(showtimes,
                  reservation => reservation.ShowtimeId,
                  showtime => showtime.Id,
                  (reservation, showtime) => new { Reservation = reservation, ShowtimeTime = showtime.Time })
            .OrderBy(joined => joined.ShowtimeTime)
            .Select(joined => joined.Reservation)
            .ToList();

        return orderedReservations;
    }


    // Adding and updating reservations
    public static void UpdateReservationList(ReservationModel res)
    {
        int index = _reservations.FindIndex(s => s.Id == res.Id);

        if (index != -1)
        {
            _reservations[index] = res;
        }
        else
        {
            _reservations.Add(res);
        }
        ReservationsAccess.WriteAll(_reservations);
    }

    public void AddReservation(ReservationModel res)
    {
        UpdateReservationList(res);
    }

    public void AddReservation(int showtimeId, int accountId, double totalPrice, List<int> seatIds)
    {
        var res = new ReservationModel(GetNextId(), seatIds, showtimeId, accountId, totalPrice, GenerateUniqueCodes(seatIds.Count));
        AddReservation(res);
    }


    // Checking and generating codes
    public bool CheckCode(string code)
    {
        return _reservations.Any(s => s.Codes.Contains(code));
    }

    public string GenerateUniqueCode()
    {
        Random random = _random;
        bool isUnique = false;
        string code = "";
        while (isUnique == false)
        {
            code = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < 6; i++)
            {
                char c = chars[random.Next(chars.Length)];
                code += c;
            }

            isUnique = !CheckCode(code);
        }
        return code;
    }


    public List<string> GenerateUniqueCodes(int amount)
    {
        List<string> codes = new List<string>();
        for (int i = 0; i < amount; i++)
        {
            codes.Add(GenerateUniqueCode());
        }
        return codes;
    }

    public int GetNextId()
    {
        return _reservations.Count == 0 ? 1 : _reservations.Max(s => s.Id) + 1;
    }

    public static List<ReservationModel> SeeFutureReservations(int accountid)
    {
        List<ReservationModel> reservations = OrderReservations();
        foreach (ReservationModel reserv in _reservations)
        {
            if (reserv.AccountId == accountid && DateTime.Now.CompareTo(ShowtimesLogic.GetShowtimeById(reserv.ShowtimeId).Time) < 0)
            {
                reservations.Add(reserv);
            }
        }
        return reservations;
    }

    public static List<ReservationModel> SeePastReservations(int accountid)
    {
        List<ReservationModel> reservations = OrderReservations();
        foreach (ReservationModel reserv in _reservations)
        {
            if (reserv.AccountId == accountid && (DateTime.Now.CompareTo(ShowtimesLogic.GetShowtimeById(reserv.ShowtimeId).Time) >= 0))
            {
                reservations.Add(reserv);
            }
        }
        return reservations;
    }

    public static ReservationModel GetReservation(string code, List<ReservationModel> reservations)
    {
        foreach (ReservationModel reser in reservations)
        {
            if (reser.Codes.Contains(code))
            {
                return reser;
            }
        }
        return null;
    }

    public static ReservationModel GetReservation(string code)
    {
        return GetReservation(code, _reservations);
    }
}
