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
}
