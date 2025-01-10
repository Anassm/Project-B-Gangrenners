public static class ManageHalls
{
    public static void ChangeSeatTypePrice()
    {
        while (true)
        {
            PresentationHelper.ClearConsole();
            PresentationHelper.PrintGreen("You have chosen to change the price of a seat type to the corresponding hall.");

            HallModel hall;
            int seatType;
            double newPrice;

            while (true)
            {
                int hallId;
                List<HallModel> halls = HallsLogic.GetAll();
                int totalHalls = halls.Count;

                if (totalHalls > 1)
                {
                    PresentationHelper.PrintYellow($"Please enter the hall id between 1 and {totalHalls} to change the seat type price.");
                    if (int.TryParse(Console.ReadLine(), out hallId))
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
                else if (totalHalls == 1)
                {
                    hall = halls.First();
                    break;
                }
                else
                {
                    Console.WriteLine("No halls found.");
                    AdminLogin.AdminMenu();
                }
            }

            while (true)
            {
                Array seatTypes = SeatsLogic.GetSeatTypes();

                PresentationHelper.PrintYellow("Please enter the number of the seat type you want to change the price for: ");
                Console.WriteLine("Available seat types: ");
                foreach (string type in seatTypes)
                {
                    Console.WriteLine($"{Array.IndexOf(seatTypes, type) + 1}. {type}");
                }

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
                PresentationHelper.PrintYellow("Please enter the new price for the seat type: ");
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
            string StartMessage = "Are you sure you want to change the price of the seat type?";
            bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
            if (YesNo)
            {
                SeatsLogic.UpdatePrice(hall.Id, seatType, newPrice);
                Console.WriteLine($"The price for seat type {seatType} in hall {hall.Id} has been successfully updated to {newPrice}.");
                AdminLogin.AdminMenu();
                return;
            }
            else
            {
                string StartMessage2 = "Do you want to go back to the main menu or change the price again?";
                SelectingMenu.MenusSelect(new string[] { "Main Menu", "Change Price Again" }, new Action[] { AdminLogin.AdminMenu, ChangeSeatTypePrice }, StartMessage2);
            }
        }
    }
}