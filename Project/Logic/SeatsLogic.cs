using System.Linq;


public class SeatsLogic
{
    private List<SeatModel> _seats;

    public SeatsLogic()
    {
        _seats = SeatsAccess.LoadAll();
    }

    public bool CheckSeatsByType(int type)
    {
        foreach (SeatModel seat in _seats)
        {
            if (seat.Id == type)
            {
                return true;
            }
        }
        return false;
    }

    public SeatModel GetSeatById(int id)
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

    public SeatModel GetSeatByRowAndSeat(int hallID, int[] seat)
    {
        return GetSeatByRowAndSeat(hallID, seat[0], seat[1]);
    }

    public SeatModel GetSeatByRowAndSeat(int hallId, int row, int seat)
    {
        return _seats.FirstOrDefault(s => s.HallId == hallId && s.Row == row && s.Seat == seat);
    }

    public SeatModel GetSeatByCoordinates(int hallId, int x, int y)
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

    public SeatModel GetSeatByCoordinates(int hallId, int[] coordinates)
    {
        return GetSeatByCoordinates(hallId, coordinates[0], coordinates[1]);
    }

    public int[] GetCoordinatesBySeat(int hallId, int seatId)
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

    public int[] GetCoordinatesBySeat(SeatModel seat)
    {
        return GetCoordinatesBySeat(seat.HallId, seat.Id);
    }

    public void UpdateList(SeatModel seat)
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
}
