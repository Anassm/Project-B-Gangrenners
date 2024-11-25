public class ReservationsLogic
{
    private static Random _random = new Random();
    private static List<ReservationModel> _reservations { get; set; } = ReservationsAccess.LoadAll();

    public ReservationsLogic()
    {
        //_reservations = ReservationsAccess.LoadAll();
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

}