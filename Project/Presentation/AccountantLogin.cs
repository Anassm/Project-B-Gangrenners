public static class AccountantLogin
{
    static private AccountantAccountsLogic accountsLogic = new AccountantAccountsLogic();

    static private int _attemptsLeft = 3;

    public static void Main()
    {
        Start();
        AccountantMenu();
    }

    public static void Start()
    {
        PresentationHelper.ClearConsole();
        if (_attemptsLeft <= 0)
        {
            PresentationHelper.PrintRed("Too many incorrect, locked out for 30 seconds.");
            Thread.Sleep(30000);
            PresentationHelper.PrintYellow("The lock out has ended, you may continue by pressing enter.");
            System.Console.ReadLine();
        }
        // This is the login page
        PresentationHelper.PrintGreen("Welcome to the accountant login page");
        PresentationHelper.PrintYellow("Please enter your email address");
        string email = Console.ReadLine();
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Please enter your password");
        string password = HideCharacter();

        // Check if the email and password match an account
        AccountantAccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine(acc.FullName + " was successfully logged in as admin ");
            AccountantMenu();
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

    public static void AccountantMenu()
    {
        string StartMessage = "What do you want to do?";
        string[] MenuNames = { "See financial data about movies", "Log out" };
        Action[] Actions = { Finance.Start, Menu.Start };
        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
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