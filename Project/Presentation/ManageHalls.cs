public static class ManageHalls
{
    public static void ChangeSeatTypePrice()
    {
        Console.Clear();
        Console.WriteLine("You have chosen to change the price of a seat type to the corresponding hall.");

        HallModel hall;
        int seatType;
        double newPrice;

        while (true)
        {
            Console.Write("Please enter the id of the hall you want to change the seat pricing for: ");
            int hallId = Convert.ToInt16(Console.ReadLine());
            hall = HallsLogic.GetHallById(hallId);

            // Check if the hall exists
            if (hall != null)
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a valid hall id.");
        }

        while (true)
        {
            Console.Write("Please enter the number of the seat type you want to change the price for: ");
            seatType = Convert.ToInt16(Console.ReadLine());

            // Check if given seat type exists in above hall
            if (hall.Seats.Exists(seat => seat.Type == seatType))
            {
                break;
            }

            Console.WriteLine("Invalid input or seat type doesn't exist in hall. Please enter a valid seat type number.");
        }

        while (true)
        {
            Console.Write("Please enter the new price for the seat type: ");
            newPrice = Convert.ToDouble(Console.ReadLine());

            if (newPrice > 0)
            {
                break;
            }

            Console.WriteLine("Price can't be lower than 0. Please enter a valid price.");
        }

        SeatsLogic.UpdatePrice(hall.Id, seatType, newPrice);

        Console.WriteLine($"The price for seat type {seatType} in hall {hall.Id} has been successfully updated to {newPrice}.");

        AdminLogin.AdminMenu();
    }
}
