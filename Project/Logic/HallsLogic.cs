public class HallsLogic
{
    private static List<HallModel> _halls { get; set; } = HallsAccess.LoadAll();

    public HallsLogic()
    {
        _halls = HallsAccess.LoadAll();
    }

    public static List<HallModel> GetAll() => _halls;

    public static HallModel GetHallById(int id)
    {
        HallModel hall = _halls.Find(hall => hall.Id == id);

        return hall;
    }

    public static int[,] GetHallLayout(int id)
    {
        HallModel hall = _halls.Find(hall => hall.Id == id);

        return hall.Layout;
    }

    public void UpdateList(HallModel hall)
    {
        int index = _halls.FindIndex(s => s.Id == hall.Id);

        if (index != -1)
        {
            _halls[index] = hall;
        }
        else
        {
            _halls.Add(hall);
        }
        HallsAccess.WriteAll(_halls);
    }

    public static (HallModel HallId, int SeatCount) GetBiggestHall()
    {
        List<SeatModel> seats = SeatsLogic.GetAll();

        HallModel biggestHall = null;
        int maxSeats = 0;

        foreach (HallModel hall in _halls)
        {
            int count = 0;
            foreach (SeatModel seat in seats)
            {
                if (seat.HallId == hall.Id)
                {
                    count++;
                }
            }

            if (count > maxSeats)
            {
                maxSeats = count;
                biggestHall = hall;
            }
        }

        return (biggestHall, maxSeats);
    }


}