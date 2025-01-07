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
        Console.Clear();
        PastReservationsMenu();
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void FutureReservations()
    {
        Console.Clear();
        FutureReservationsMenu();
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void AllReservations()
    {
        Console.Clear();
        AllReservationsMenu();
        PresentationHelper.PrintYellow("Press any key to continue.");
        System.Console.WriteLine();
        PresentationHelper.PressAnyToContinue(SeeReservationSubMenu);
    }

    public static void ReservationByCode()
    {
        Console.Clear();
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
        PresentationHelper.PrintYellow("Enter the code of the reservation you want to see:");
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