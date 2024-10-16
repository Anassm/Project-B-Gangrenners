public class ReservationsLogic
{
    private List<ReservationModel> _reservations;

    public ReservationsLogic()
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
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        for (int i = 0; i < 6; i++)
        {
            char c = chars[random.Next(chars.Length)];
            code += c;
        }
        //check if the code is already in use
        if (_reservations.FindIndex(s => s.Code == code) != -1)
        {
            return GenerateCode();
        }

        return code;
    }

    public int GetNextId()
    {
        return _reservations.Count == 0 ? 1 : _reservations.Max(s => s.Id) + 1;
    }

}