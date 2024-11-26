public class HallsLogic
{
    private static List<HallModel> _halls { get; set; } = HallsAccess.LoadAll();

    public HallsLogic()
    {
        _halls = HallsAccess.LoadAll();
    }

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
}