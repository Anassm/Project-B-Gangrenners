public static class SeeReservations
{
    public static void SeeReservationSubMenu()
    {
        string StartMessage = "You have chosen to see your reservations.\nWould you like to see:";
        string[] MenuNames = { "Past reservations", "Future reservations", "All reservations", "See reservations by code", "Go back" };
        Action[] Actions = {PastReservations, FutureReservations, AllReservations, ReservationByCode, Menu.MainMenu};
        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void PastReservations()
    {
        PresentationHelper.ClearConsole();
        PastReservationsMenu();
        
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void FutureReservations()
    {
        PresentationHelper.ClearConsole();
        FutureReservationsMenu();
        System.Console.WriteLine("If you want to add food, drinks or products to a reservation, enter the code of the reservation you want to add to. If you do not want to add anything, press enter to go back.");
        string code = Console.ReadLine();
        if (string.IsNullOrEmpty(code))
        {
            SeeReservationSubMenu();
            return;
        }

        ReservationModel reservation = ReservationsLogic.GetReservation(code);
        if (reservation != null && ShowtimesLogic.GetShowtimeById(reservation.ShowtimeId).Time > DateTime.Now)
        {
            BuyExtras.ProductMenu(false, reservation);
        }
        else
        {
            System.Console.WriteLine("Reservation not found or it is not a future reservation.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to continue.");
            PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
        }
    
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void AllReservations()
    {
        PresentationHelper.ClearConsole();
        AllReservationsMenu();
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void ReservationByCode()
    {
        PresentationHelper.ClearConsole();
        SeeReservationByCode();
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to continue.");
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
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
                System.Console.WriteLine(reservation.ToStringWithSeatsAndOrder());
                System.Console.WriteLine("-----------------------------------");
            }
            return;
        }
        PresentationHelper.PrintRed("No reservations at the moment.");
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
                System.Console.WriteLine(reservation.ToStringWithSeatsAndOrder());
                System.Console.WriteLine("-----------------------------------");
            }
            return;
        }
        PresentationHelper.PrintRed("No reservations at the moment.");
    }

    public static void AllReservationsMenu()
    {
        PastReservationsMenu();
        System.Console.WriteLine("");
        FutureReservationsMenu();
    }

    public static void SeeReservationByCode()
    {
        PresentationHelper.PrintYellow("Enter the code of the reservation you want to see:");
        string code = Console.ReadLine();
        ReservationModel reservation = ReservationsLogic.GetReservation(code);
        if (reservation != null)
        {
            System.Console.WriteLine(reservation.ToStringWithSeatsAndOrder());
        }
        else
        {
            PresentationHelper.PrintRed("Reservation not found.");
        }
    }

    public static void GetUpdatedReservation(ReservationModel reservation)
    {
        System.Console.WriteLine("Your reservation has been updated.");
        System.Console.WriteLine("This is what it looks like");
        System.Console.WriteLine(reservation.ToStringWithSeatsAndOrder());
        System.Console.WriteLine("Press any key to continue.");
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }
}