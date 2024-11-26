public class Register
{

    public static void Start()
    {
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        bool emailCheck = AccountsLogic.CheckEmail(email);
        if (!emailCheck)
        {
            Console.Clear();
            Console.WriteLine("Invalid email address");
            Start();
            return;
        }
        string password = "";
        bool totalCheck = false;
        while (!totalCheck)
        {
            Console.WriteLine("Please enter your password");
            password = HideCharacter();
            bool passwordCheck = AccountsLogic.CheckPassword(password);
            if (!passwordCheck)
            {
                continue;
                Console.Clear();
            }
            Console.WriteLine("\nPlease confirm your password");
            string passwordConfirm = HideCharacter();
            if (password != passwordConfirm)
            {
                Console.Clear();
                Console.WriteLine("\nPasswords do not match");
            }
            else if (password == passwordConfirm && passwordCheck)
            {
                totalCheck = true;
            }
        }
        
        Console.WriteLine("\nPlease enter your first name");
        string firstName = Console.ReadLine();
        Console.WriteLine("Please enter your last name");
        string lastName = Console.ReadLine();
        Console.WriteLine("Please enter your date of birth");
        string dateofbirth = Console.ReadLine();
        DateTime dob = DateTime.Parse(dateofbirth);
        AccountsLogic.AddAccount(email, password, firstName, lastName, dob);
        Console.Clear();
        Console.WriteLine("Account created successfully");
        Menu.Start();

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