public class Register
{
    public static void Start()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintYellow("Please enter your email address");
        string email = Console.ReadLine().ToLower();
        if (!AccountsLogic.CheckEmail(email))
        {
            // Email is already in use
            if(email == "")
            {
                Console.WriteLine("Email address cannot be empty");
            }
            else if (email.Length < 5)
            {
                Console.WriteLine("Email address is too short");
            }
            else if (AccountsLogic.CheckEmailInUse(email))
            {
                Console.WriteLine("Email address is already in use");
            }
            else
            {
                Console.WriteLine("Email address must contain an @ and a .");
            }
            Start();
            return;
        }

        string password = GetValidPassword();

        string firstName = GetValidFirstName();
        
        string lastName = GetValidLastName();

        DateTime dateOfBirth = GetValidDateOfBirth();

        AccountsLogic.AddAccount(email, password, firstName, lastName, dateOfBirth);
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("Account created successfully");
        System.Console.WriteLine();
        PresentationHelper.PrintYellow("Press any key to continue");
        PresentationHelper.PressAnyToContinue(Menu.Start);
    }

    private static string GetValidFirstName()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            PresentationHelper.PrintYellow("Please enter your first name");
            string firstName = Console.ReadLine();
            if (AccountsLogic.CheckName(firstName))
            {
                return firstName;
            }
            PresentationHelper.ClearConsole();
            Console.WriteLine("Invalid first name");
        }
    }

    private static string GetValidLastName()
    {
        while (true)
        {
            PresentationHelper.PrintYellow("Please enter your last name");
            string lastName = Console.ReadLine();
            if (AccountsLogic.CheckName(lastName))
            {
                return lastName;
            }
            Console.WriteLine("Invalid last name");
        }
    }


    private static string GetValidPassword()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            PresentationHelper.PrintYellow("Please enter your password, must be at least 8 characters long and contain at least one number, one uppercase letter and one special character");
            string password = HideCharacter();
            if (!AccountsLogic.CheckPassword(password))
            {
                PresentationHelper.ClearConsole();
                Console.WriteLine("Invalid password");
                continue;
            }
            PresentationHelper.PrintYellow("\nPlease confirm your password");
            string passwordConfirm = HideCharacter();
            if (password == passwordConfirm)
            {
                return password;
            }

            Console.WriteLine("\nPasswords do not match");
        }
    }

    private static DateTime GetValidDateOfBirth()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            PresentationHelper.PrintYellow("Please enter your date of birth (dd-MM-yyyy)");
            string dateOfBirthInput = Console.ReadLine();
            if (AccountsLogic.CheckDateOfBirth(dateOfBirthInput))
            {
                return DateTime.Parse(dateOfBirthInput);
            }

            Console.WriteLine("Invalid date of birth");
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
