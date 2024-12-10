public static class AdminLogin
{
    static private AdminAccountsLogic accountsLogic = new AdminAccountsLogic();
    static private int _attemptsLeft = 3;

    public static void Main()
    {
        Start();
        AdminMenu();
    }

    public static void Start()
    {
        if (_attemptsLeft <= 0)
        {
            System.Console.WriteLine("Too many incorrect, locked out for 30 seconds.");
            Thread.Sleep(30000);
            System.Console.WriteLine("The lock out has ended, you may continue by pressing enter.");
            System.Console.ReadLine();
        }
        // This is the login page
        Console.WriteLine("Welcome to the admin login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = HideCharacter();

        // Check if the email and password match an account
        AdminAccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine(acc.FullName + " was successfully logged in as admin ");
            AdminMenu();
        }
        else
        {
            Console.Clear();
            _attemptsLeft--;
            if (_attemptsLeft < 0)
            {
                _attemptsLeft = 0;
            }
            Console.WriteLine("No admin account found with that email and/or password");
            Console.WriteLine($"{_attemptsLeft} attempts left.");
            Menu.Start();
        }
    }

    public static void AdminMenu()
    {
        System.Console.WriteLine("What do you want to do?");
        System.Console.WriteLine("1. Add a movie");
        System.Console.WriteLine("2. Remove movie");
        System.Console.WriteLine("3. Promote movie");
        System.Console.WriteLine("4. Demote movie");
        System.Console.WriteLine("5. See current movies");
        System.Console.WriteLine("6. See archived movies");
        System.Console.WriteLine("7. Change seat type price for hall");
        System.Console.WriteLine("8. Add single showtime.");
        System.Console.WriteLine("9. Add multiple showtimes");
        System.Console.WriteLine("10. See financial data about movies");
        System.Console.WriteLine("11. Log out");
        string input = System.Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                ManageMovies.AddMovieMenu();
                break;
            case "2":
                Console.Clear();
                ManageMovies.RemoveMovieMenu();
                break;
            case "3":
                Console.Clear();
                ManageMovies.PromoteMovieMenu();
                break;
            case "4":
                Console.Clear();
                ManageMovies.DemoteMovieMenu();
                break;
            case "5":
                Console.Clear();
                ManageMovies.SeeCurrentMoviesMenu();
                break;
            case "6":
                Console.Clear();
                ManageMovies.SeeArchivedMoviesMenu();
                break;
            case "7":
                ManageHalls.ChangeSeatTypePrice();
                break;
            case "8":
                ManageMovies.AddSingleShowTimeMenu();
                break;
            case "9":
                ManageMovies.AddShowTimesMenu();
                break;
            case "10":
                Console.Clear();
                Finance.Start();
                break;
            case "11":
                _attemptsLeft = 3;
                Console.Clear();
                Menu.Start();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
                AdminMenu();
                break;
        }
    }


    public static string HideCharacter()
    {
        ConsoleKeyInfo key;
        string code = "";
        do
        {
            key = Console.ReadKey(true);

            if (Char.IsNumber(key.KeyChar) || Char.IsLetter(key.KeyChar) || Char.IsPunctuation(key.KeyChar) || Char.IsSymbol(key.KeyChar))
            {
                Console.Write("*");
            }
            if (key.Key == ConsoleKey.Backspace && code.Length > 0)
            {
                code = code.Remove(code.Length - 1);
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Enter)
            {
                code += key.KeyChar;
            }
        } while (key.Key != ConsoleKey.Enter);

        return code;

    }
}