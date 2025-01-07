public static class AboutPage
{
    public static void Start()
    {
        string StartMessage = "Welcome to the about page!\nPlease select an option you would like to be informed about:";
        string[] MenuNames = {"Accessability", "Opening hours", "General information", "Contact information", "Go back to main menu"};
        Action[] Actions = {Accessability, OpeningHours, GeneralInformation, Contact, Menu.Start,};
        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void Accessability()
    {
        PresentationHelper.ClearConsole();
        var parkingSpots = new Dictionary<string, (string Address, decimal CostPerHour)>
        {
            { "Central Parking", ("123 Main St", 5.50m) },
            { "West End Lot", ("456 Elm St", 4.00m) },
            { "East Side Garage", ("789 Oak Ave", 6.25m) }
        };

        PresentationHelper.PrintGreen("Accessability");
        Console.WriteLine("Parking");
        Console.WriteLine("------------------");

        foreach (var spot in parkingSpots)
        {
            Console.WriteLine($"Name: {spot.Key}");
            Console.WriteLine($"Address: {spot.Value.Address}");
            Console.WriteLine($"Cost per hour: €{spot.Value.CostPerHour:F2}");
            System.Console.WriteLine();
        }

        Console.WriteLine("Public Transport");
        Console.WriteLine("------------------");
        Console.WriteLine("Tram: If you are coming to our cinema by public transport, you could buy a ticket online for the tram heading to tram/bus stop `Gangrenners`.");
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to return to the main menu");
        PresentationHelper.PressAnyToContinue(Start);
    }

    public static void OpeningHours()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("Opening hours");
        Console.WriteLine("Everyday: Our cinema opens 15 minutes before the first showtime, and closes 30 minutes after the last showtime.");
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to return to the main menu");
        PresentationHelper.PressAnyToContinue(Start);
    }

    public static void GeneralInformation()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("General information");

        int seatsAmount = SeatsLogic.GetAll().Count;
        int hallsAmount = HallsLogic.GetAll().Count;
        (HallModel HallId, int SeatCount) biggestHall = HallsLogic.GetBiggestHall();

        Console.WriteLine($"Address: 1234AB Rotterdam gangrennersstreet 123\n");
        Console.WriteLine($"Seats: {seatsAmount} seats\n");
        Console.WriteLine($"Biggest hall: {biggestHall.HallId.Id}, {biggestHall.SeatCount} seats\n");
        Console.WriteLine($"Halls: {hallsAmount} halls\n");
        Console.WriteLine("Sound system: Dolby Gangrenners Atmos special\n");
        Console.WriteLine("Support: Wheelchair, Auditory and visual support, Found objects and Nuisance SMS code: SMS 'GANGRENNERS' 'room number' 'reason for support' to 1234");
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to return to the main menu");
        PresentationHelper.PressAnyToContinue(Start);
    }

    public static void Contact()
    {
        PresentationHelper.ClearConsole();
        Console.WriteLine("Contact");
        Console.WriteLine("Phone: 123-456-7890");
        Console.WriteLine("Email: service@gangrenners.com");
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to return to the main menu");
        PresentationHelper.PressAnyToContinue(Start);
    }
}
