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
