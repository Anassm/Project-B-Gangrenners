static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        PresentationHelper.ClearConsole();
        // This is the login page
        PresentationHelper.PrintGreen("Welcome to the login page");
        Console.WriteLine();
        PresentationHelper.PrintYellow("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine();
        PresentationHelper.PrintYellow("Please enter your password");
        string password = HideCharacter();
        
        // Check if the email and password match an account
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            PresentationHelper.PrintGreen("Welcome back " + acc.FirstName + " " + acc.LastName);
            Console.WriteLine("Your email is " + acc.EmailAddress);
            PresentationHelper.ClearConsole();
            Menu.MainMenu();
        }
        else
        {
            PresentationHelper.ClearConsole();
            PresentationHelper.PrintRed("No account found with that email and/or password");
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