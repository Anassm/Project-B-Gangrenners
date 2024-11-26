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
            if (int.TryParse(Console.ReadLine(), out int hallId))
            {
                hall = HallsLogic.GetHallById(hallId);

                // Check if the hall exists
                if (hall != null)
                {
                    break;
                }

                Console.WriteLine("Invalid input. No hall found with the given id.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric hall id.");
            }
        }

        while (true)
        {
            Console.Write("Please enter the number of the seat type you want to change the price for: ");
            if (int.TryParse(Console.ReadLine(), out seatType))
            {
                // Check if given seat type exists in the hall
                if (SeatsLogic.CheckSeatsByType(hall.Id, seatType))
                {
                    break;
                }

                Console.WriteLine("Invalid input or seat type doesn't exist in hall. Please enter a valid seat type number.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric seat type.");
            }
        }

        while (true)
        {
            Console.Write("Please enter the new price for the seat type: ");
            if (double.TryParse(Console.ReadLine(), out newPrice))
            {
                // Price can't be lower than 0
                if (newPrice > 0)
                {
                    break;
                }

                Console.WriteLine("Price can't be lower than 0. Please enter a valid price.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric price.");
            }
        }

        SeatsLogic.UpdatePrice(hall.Id, seatType, newPrice);

        Console.WriteLine($"The price for seat type {seatType} in hall {hall.Id} has been successfully updated to {newPrice}.");

        AdminLogin.AdminMenu();
    }
}
