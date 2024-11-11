static class AdminLogin
{
    static private AdminAccountsLogic accountsLogic = new AdminAccountsLogic();
    static private int _attemptsLeft = 3;
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
            Console.WriteLine( acc.FullName + "was successfully logged in as admin ");
            Console.Clear();
            Menu.MainMenu();
        }
        else
        {
            Console.Clear();
            _attemptsLeft--;
            Console.WriteLine("No admin account found with that email and/or password");
            Console.WriteLine($"{_attemptsLeft} attempts left." );
            Menu.Start();
        }
    }

    public static string HideCharacter()
    {
        ConsoleKeyInfo key;
        string code = "";
        do
        {
            key = Console.ReadKey(true);

            if (Char.IsNumber(key.KeyChar) || Char.IsLetter(key.KeyChar))
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