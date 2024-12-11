public static class SeeReservations
{
    public static void SeeReservationSubMenu()
    {
        System.Console.WriteLine("You have chosen to see your reservations.");
        System.Console.WriteLine("would you like to see:");
        System.Console.WriteLine("1. Past reservations");
        System.Console.WriteLine("2. Future reservations");
        System.Console.WriteLine("3. All reservations");
        System.Console.WriteLine("4. See reservation by code");
        System.Console.WriteLine("5. Go back");
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
                SeeReservationByCode();
                System.Console.WriteLine("Press any key to continue.");
                ConsoleKeyInfo key4 = Console.ReadKey(true);
                if (key4.Key != null)
                {
                    Console.Clear();
                    SeeReservationSubMenu();
                }
                break;
            case "5":
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
        List<ReservationModel> _reservations = ReservationsLogic.SeeFutureReservations(AccountsLogic.CurrentAccount.Id);
        System.Console.WriteLine("All future reservations:");
        System.Console.WriteLine("-----------------------------------");
        if (_reservations.Count > 0)
        {

            foreach (ReservationModel reservation in _reservations)
            {
                System.Console.WriteLine(reservation.ToStringWithSeats());
                System.Console.WriteLine("-----------------------------------");
            }
            return;
        }
        System.Console.WriteLine("No reservations at the moment.");
    }

    public static void PastReservationsMenu()
    {
        List<ReservationModel> _reservations = ReservationsLogic.SeePastReservations(AccountsLogic.CurrentAccount.Id);
        System.Console.WriteLine("All past reservations:");
        System.Console.WriteLine("-----------------------------------");
        if (_reservations.Count > 0)
        {
            foreach (ReservationModel reservation in _reservations)
            {
                System.Console.WriteLine(reservation.ToStringWithSeats());
                System.Console.WriteLine("-----------------------------------");
            }
            return;
        }
        System.Console.WriteLine("No reservations at the moment.");
    }

    public static void AllReservationsMenu()
    {
        PastReservationsMenu();
        System.Console.WriteLine("");
        FutureReservationsMenu();
    }

    public static void SeeReservationByCode()
    {
        System.Console.WriteLine("Enter the code of the reservation you want to see:");
        string code = Console.ReadLine();
        ReservationModel reservation = ReservationsLogic.GetReservation(code);
        if (reservation != null)
        {
            System.Console.WriteLine(reservation.ToStringWithSeats());
        }
        else
        {
            System.Console.WriteLine("Reservation not found.");
        }
    }
}