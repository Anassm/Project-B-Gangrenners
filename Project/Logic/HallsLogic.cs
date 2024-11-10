public class HallsLogic
{
    private List<HallModel> _halls;

    public HallsLogic()
    {
        _halls = HallsAccess.LoadAll();
    }

    public HallModel GetHallById(int id)
    {
        HallModel hall = _halls.Find(hall => hall.Id == id);

        return hall;
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