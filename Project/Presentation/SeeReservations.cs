public static class SeeReservations
{
    public static void SeeReservationSubMenu()
    {
        System.Console.WriteLine("You have chosen to see your reservations.");
        System.Console.WriteLine("would you like to see:");
        System.Console.WriteLine("1. Past reservations");
        System.Console.WriteLine("2. Future reservations");
        System.Console.WriteLine("3. All reservations");
        System.Console.WriteLine("4. Quit");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "1":
                Console.Clear();
                PastReservationsMenu();
                System.Console.WriteLine("Press any key to continue.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != null)
                {
                    Console.Clear();
                    SeeReservationSubMenu();
                }
                break;
            case "2":
                Console.Clear();
                FutureReservationsMenu();
                System.Console.WriteLine("Press any key to continue.");
                ConsoleKeyInfo key2 = Console.ReadKey(true);
                if (key2.Key != null)
                {
                    Console.Clear();
                    SeeReservationSubMenu();
                }
                break;
            case "3":
                Console.Clear();
                AllReservationsMenu();
                System.Console.WriteLine("Press any key to continue.");
                ConsoleKeyInfo key3 = Console.ReadKey(true);
                if (key3.Key != null)
                {
                    Console.Clear();
                    SeeReservationSubMenu();
                }
                break;
            case "4":
                Console.Clear();
                Menu.MainMenu();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
                System.Console.WriteLine("Press any key.");
                ConsoleKeyInfo key5 = Console.ReadKey(true);
                if (key5.Key != null)
                {
                    Console.Clear();
                    SeeReservationSubMenu();
                }
                break;
        }
    }

    public static void FutureReservationsMenu()
    {
        System.Console.WriteLine("All future reservations:");
        System.Console.WriteLine("-----------------------------------");
        foreach (ReservationModel reservation in ReservationsLogic.SeeFutureReservations(AccountsLogic.CurrentAccount.Id))
        {
            System.Console.WriteLine(reservation.ToString());
            System.Console.WriteLine("-----------------------------------");
        }
    }

    public static void PastReservationsMenu()
    {
        System.Console.WriteLine("All past reservations:");
        System.Console.WriteLine("-----------------------------------");
        foreach (ReservationModel reservation in ReservationsLogic.SeePastReservations(AccountsLogic.CurrentAccount.Id))
        {
            System.Console.WriteLine(reservation.ToString());
            System.Console.WriteLine("-----------------------------------");
        }
    }

    public static void AllReservationsMenu()
    {
        PastReservationsMenu();
        System.Console.WriteLine("");
        FutureReservationsMenu();
    }
}