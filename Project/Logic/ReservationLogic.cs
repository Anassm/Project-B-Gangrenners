public class ReservationLogic
{
    private List<ReservationModel> _reservations;

    public ReservationLogic()
    {
        _reservations = ReservationsAccess.LoadAll();
    }

    public void UpdateList(ReservationModel res)
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

    public string GenerateCode()
    {
        string code = "";
        Random random = new Random(0);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 6; i++)
        {
            char c = chars[random.Next(chars.Length)];
            code += c;
        }
        return code;
    }

}