public static class PresentationHelper
{
    public static void ClearConsole()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }

    public static void PressAnyToContinue(Action method)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            method();
        }
    }

    public static void PrintGray(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintRed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintGreen(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintYellow(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(message);
        Console.ResetColor();
    }

    public static bool Continue(Action method)
    {
        PresentationHelper.PrintYellow("Do you want to continue? (Y/N)");
        string input = Console.ReadLine().ToLower();
        if (input == "n" || input == "no")
        {
            Console.Clear();
            method();
        }
        if (input == "y" || input == "yes")
        {
            return true;
        }
        else
        {
            Console.Clear();
            PrintRed("Invalid input.");
            return false;
        }
    }

    public static int AlteredContinue()
    {
        string input = Console.ReadLine().ToLower();
        if (input == "n" || input == "no")
        {
            return 0;
        }
        if (input == "y" || input == "yes")
        {
            return 1;
        }
        else
        {
            Console.Clear();
            PrintRed("Invalid input.");
            return -1;
        }
    }

    public static string StringInput(Action method)
    {
        string input = System.Console.ReadLine();
        if (input == "")
        {
            ClearConsole();
            PrintRed("Can not input nothing, please give an input.");
            return "";
        }
        else
        {
            return input;
        }
    }

    public static int IntInput(Action method)
    {
        string Input = System.Console.ReadLine().ToLower();
        if (Input.All(char.IsDigit))
        {
            int ConvertedInput = Convert.ToInt32(Input);
            if (ConvertedInput <= 0)
            {
                Console.Clear();
                PrintRed("Input has to be larger than 0.");
                return 0;
            }
            return ConvertedInput;
        }
        else
        {
            Console.Clear();
            PrintRed("Invalid input, Enter a number.");
            return 0;
        }
    }
}