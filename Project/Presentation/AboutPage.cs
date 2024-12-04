public static class AboutPage
{

    public static void Start()
    {
        Console.WriteLine("Welcome to the about page!");
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Data about cinema");
        Console.WriteLine("2. Contact");
        Console.WriteLine("3. Go back to main menu");

        // Make sure to also do validation for inputs   
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                HowItWorks();
                break;
            case "2":
                Console.Clear();
                DataAboutCinema();
                break;
            case "3":
                Console.Clear();
                Contact();
                break;
            case "4":
                Console.Clear();
                Menu.Start();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                Start();
                break;
        }
    }
}

// The user is shown an about page with information about the cinema and how the reservation system works.
public static void HowItWorks()
{

}

// Display the address, parking, number of halls, number of seats
public static void DataAboutCinema()
{

}

// The about page provides contact information for customer support or feedback.
public static void Contact()
{

}
}