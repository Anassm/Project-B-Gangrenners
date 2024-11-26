public class Register
{
    public static void Start()
    {
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        if (!AccountsLogic.CheckEmail(email))
        {
            Console.WriteLine("Invalid email address");
            Start();
            return;
        }

        string password = GetValidPassword();

        string firstName = GetValidFirstName();
        
        string lastName = GetValidLastName();
        
        DateTime dateOfBirth = GetValidDateOfBirth();

        AccountsLogic.AddAccount(email, password, firstName, lastName, dateOfBirth);
        Console.WriteLine("Account created successfully");
        Menu.Start();
    }

    private static string GetValidFirstName()
    {
        while (true)
        {
            Console.WriteLine("Please enter your first name");
            string firstName = Console.ReadLine();
            if (firstName.Length < 2)
            {
                Console.WriteLine("Invalid first name");
                continue;
            }

            return firstName;
        }
    }

    private static string GetValidLastName()
    {
        while (true)
        {
            Console.WriteLine("Please enter your last name");
            string lastName = Console.ReadLine();
            if (lastName.Length < 2)
            {
                Console.WriteLine("Invalid last name");
                continue;
            }

            return lastName;
        }
    }


    private static string GetValidPassword()
    {
        while (true)
        {
            Console.WriteLine("Please enter your password");
            string password = HideCharacter();
            if (!AccountsLogic.CheckPassword(password))
            {
                Console.WriteLine("Invalid password");
                continue;
            }

            Console.WriteLine("\nPlease confirm your password");
            string passwordConfirm = HideCharacter();
            if (password == passwordConfirm)
            {
                return password;
            }

            Console.WriteLine("Passwords do not match");
        }
    }

    private static DateTime GetValidDateOfBirth()
    {
        while (true)
        {
            Console.WriteLine("Please enter your date of birth (dd-MM-yyyy)");
            string dateOfBirthInput = Console.ReadLine();
            try
            {
                DateTime dateOfBirth = DateTime.Parse(dateOfBirthInput);
                bool isWithinValidRange = dateOfBirth <= DateTime.Now && dateOfBirth >= DateTime.Now.AddYears(-120);

                if (isWithinValidRange)
                {
                    return dateOfBirth;
                }
            }
            catch (FormatException)
            {
                // Do nothing, will prompt for input again
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
