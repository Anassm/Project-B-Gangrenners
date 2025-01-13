using System.Linq;


public class SeatsLogic
{
    private static List<SeatModel> _seats { get; set; } = SeatsAccess.LoadAll();
    private static string[] _seatTypes = { "Regular", "VIP", "VIP+" };

    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();

    public SeatsLogic()
    {
        _seats = SeatsAccess.LoadAll();
    }

    public static void UpdateAvailability(ShowtimeModel showtime, List<SeatModel> seats)
    {
        foreach(SeatModel seat in seats)
        {
            _showtimesLogic.ReserveSeat(showtime.Id, seat.Row, seat.Seat);
            int[] coordinates = SeatsLogic.GetCoordinatesBySeat(seat);
            showtime.Availability[coordinates[0], coordinates[1]] = 1;
        }
        _showtimesLogic.UpdateList(showtime);
    }

    public static void ResetAvailability(ShowtimeModel showtime, List<SeatModel> seats)
    {
        foreach (SeatModel seat in seats)
        {
            int[] coordinates = SeatsLogic.GetCoordinatesBySeat(seat);
            showtime.Availability[coordinates[0], coordinates[1]] = 0;
        }
    }
    public static List<int> MakeSeatList(List<SeatModel> seats)
    {
        List<int> seatIds = new List<int>();
        foreach (SeatModel seat in seats)
        {
            seatIds.Add(seat.Id);
        }
        return seatIds;
    }
    public static double CalculateTotalPrice(List<SeatModel> seats)
    {
        double totalPrice = 0;
        foreach (SeatModel seat in seats)
        {
            totalPrice += seat.Price;
        }
        return totalPrice;
    }

    public static string GetSeatTypes(List<SeatModel> seats)
    {
        int countNormal = 0;
        int countVIP = 0;
        int countVIPPlus = 0;
        foreach (SeatModel seat in seats)
        {
            if (seat.Type == 1)
            {
                countNormal++;
            }
            else if (seat.Type == 2)
            {
                countVIP++;
            }
            else if (seat.Type == 3)
            {
                countVIPPlus++;
            }
        }
        string seatTypes = "";
        if (countNormal > 0)
        {
            seatTypes += countNormal + "x normal";
        }
        if (countVIP > 0)
        {
            if (countNormal > 0)
            {
                seatTypes += ", ";
            }
            seatTypes += countVIP + "x VIP";
        }
        if (countVIPPlus > 0)
        {
            if (countNormal > 0 || countVIP > 0)
            {
                seatTypes += ", ";
            }
            seatTypes += countVIPPlus + "x VIP+";
        }

        if (seats.Count > 1)
        {
            seatTypes += " seats";
        }
        else
        {
            seatTypes += " seat";
        }
        return seatTypes;
    }

    public static List<SeatModel> GetAll() => _seats;

    public bool CheckSeatsByType(int type)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.Type == type)
            {
                return true;
            }
        }
        return false;
    }

    public static Array GetSeatTypes() => _seatTypes;

    public static bool CheckSeatsByType(int hallId, int type)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.HallId == hallId && seat.Type == type)
            {
                return true;
            }
        }
        return false;
    }

    public static SeatModel GetSeatById(int id)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.Id == id)
            {
                return seat;
            }
        }
        return null;
    }

    public double GetPriceBySeat(int id)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.Id == id)
            {
                return seat.Price;
            }
        }
        return 0;
    }

    public double GetPriceBySeat(SeatModel seat)
    {
        return seat.Price;
    }

    public List<SeatModel> GetSeatsByHall(int hallId)
    {
        List<SeatModel> seats = new List<SeatModel>();

        foreach (SeatModel seat in _seats)
        {
            if (seat.HallId == hallId)
            {
                seats.Add(seat);
            }
        }
        return seats;
    }

    public static SeatModel GetSeatByRowAndSeat(int hallID, int[] seat)
    {
        return GetSeatByRowAndSeat(hallID, seat[0], seat[1]);
    }

    public static SeatModel GetSeatByRowAndSeat(int hallId, int row, int seat)
    {
        return _seats.FirstOrDefault(s => s.HallId == hallId && s.Row == row && s.Seat == seat);
    }

    public static SeatModel GetSeatByCoordinates(int hallId, int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return null;
        }

        if (hallId == 1)
        {
            return _seats.FirstOrDefault(s => s.HallId == hallId && s.Row == 14 - x && s.Seat == y + 1);
        }
        else if (hallId == 2)
        {
            return _seats.FirstOrDefault(s => s.HallId == hallId && s.Row == 19 - x && s.Seat == y + 1);
        }
        else if (hallId == 3)
        {
            return _seats.FirstOrDefault(s => s.HallId == hallId && s.Row == 20 - x && s.Seat == y + 1);
        }
        return null;
    }

    public static SeatModel GetSeatByCoordinates(int hallId, int[] coordinates)
    {
        return GetSeatByCoordinates(hallId, coordinates[0], coordinates[1]);
    }

    public static int[] GetCoordinatesBySeat(int hallId, int seatId)
    {
        SeatModel seat = _seats.FirstOrDefault(s => s.HallId == hallId && s.Id == seatId);
        if (seat == null)
        {
            return null;
        }
        if (hallId == 1)
        {
            return new int[] { 14 - seat.Row, seat.Seat - 1 };
        }
        else if (hallId == 2)
        {
            return new int[] { 19 - seat.Row, seat.Seat - 1 };
        }
        else if (hallId == 3)
        {
            return new int[] { 20 - seat.Row, seat.Seat - 1 };
        }
        return null;
    }

    public static int[] GetCoordinatesBySeat(SeatModel seat)
    {
        return GetCoordinatesBySeat(seat.HallId, seat.Id);
    }

    public static void UpdateList(SeatModel seat)
    {
        int index = _seats.FindIndex(s => s.Id == seat.Id);

        if (index != -1)
        {
            _seats[index] = seat;
        }
        else
        {
            _seats.Add(seat);
        }
        SeatsAccess.WriteAll(_seats);
    }

    // In a hall, change the price for all seats with a specific type
    public static void UpdatePrice(int hallId, int type, double price)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.HallId == hallId && seat.Type == type)
            {
                seat.Price = price;
            }
        }

        SeatsAccess.WriteAll(_seats);
    }
}
