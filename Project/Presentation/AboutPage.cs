public static class AboutPage
{
    public static void Start()
    {
        Console.WriteLine("Welcome to the about page!");
        Console.WriteLine("Please select an option you would like to be informed about:");
        Console.WriteLine("1. Accessability");
        Console.WriteLine("2. Opening hours");
        Console.WriteLine("3. General information");
        Console.WriteLine("4. Contact");
        Console.WriteLine("5. Go back to main menu");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Accessability();
                break;
            case "2":
                Console.Clear();
                OpeningHours();
                break;
            case "3":
                Console.Clear();
                GeneralInformation();
                break;
            case "4":
                Console.Clear();
                Contact();
                break;
            case "5":
                Console.Clear();
                Menu.MainMenu();
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }
    }

    public static void Accessability()
    {
        var parkingSpots = new Dictionary<string, (string Address, decimal CostPerHour)>
        {
            { "Central Parking", ("123 Main St", 5.50m) },
            { "West End Lot", ("456 Elm St", 4.00m) },
            { "East Side Garage", ("789 Oak Ave", 6.25m) }
        };

        Console.WriteLine("Accessability");
        Console.WriteLine("Parking");
        Console.WriteLine("------------------");

        foreach (var spot in parkingSpots)
        {
            Console.WriteLine($"Name: {spot.Key}");
            Console.WriteLine($"Address: {spot.Value.Address}");
            Console.WriteLine($"Cost per hour: ${spot.Value.CostPerHour:F2}");
            Console.WriteLine();
        }

        Console.WriteLine("Public Transport");
        Console.WriteLine("------------------");
        Console.WriteLine("Tram: If you are coming to our cinema by public transport, you could buy a ticket online for the tram heading to tram/bus stop `Gangrenners`.");

        AskToKnowMore();
    }

    public static void OpeningHours()
    {
        Console.WriteLine("Opening hours");
        Console.WriteLine("Everyday: Our cinema opens 15 minutes before the first showtime, and closes 30 minutes after the last showtime.");

        AskToKnowMore();
    }

    public static void GeneralInformation()
    {
        Console.WriteLine("General information");

        int seatsAmount = SeatsLogic.GetAll().Count;
        int hallsAmount = HallsLogic.GetAll().Count;
        (HallModel HallId, int SeatCount) biggestHall = HallsLogic.GetBiggestHall();

        Console.WriteLine($"Seats: {seatsAmount}\n");
        Console.WriteLine($"Biggest hall: Hall {biggestHall.HallId}: {biggestHall.SeatCount} seats\n");
        Console.WriteLine($"Halls: {hallsAmount}\n");
        Console.WriteLine("Sound system: Dolby Gangrenners Atmos special\n");
        Console.WriteLine("Support: Wheelchair, Auditory and visual support, Found objects and Nuisance SMS code: SMS 'GANGRENNERS' 'room number' 'reason for support' to 1234");

        AskToKnowMore();
    }

    public static void Contact()
    {
        Console.WriteLine("Contact");
        Console.WriteLine("Phone: 123-456-7890");
        Console.WriteLine("Email: service@gangrenners.com");

        AskToKnowMore();
    }

    private static void AskToKnowMore()
    {
        Console.WriteLine("\nWould you like to:");
        Console.WriteLine("1. Ask more about the cinema");
        Console.WriteLine("2. Go back to the main menu");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Start();
                break;
            case "2":
                Console.Clear();
                Menu.MainMenu();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                AskToKnowMore();
                break;
        }
    }
}
