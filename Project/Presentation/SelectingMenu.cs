public static class SelectingMenu
{
    public static  void MenusSelect(string[] menuOptions, Action[] menuActions)
    {
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i+1}. {menuOptions[i]}");
                    Console.ResetColor();
                    
                }
                else
                {
                    Console.WriteLine($"{i+1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length -1) ? 0 : selectedIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    // Handle selection
                    menuActions[selectedIndex]();
                    break;
                
                default:
                    break;
            }
        }
    }

    public static bool YesNoSelect(string message)
    {
        int selectedIndex = 0;
        String[] menuOptions = {"Yes", "No"};

        while (true)
        {
            Console.Clear();
            System.Console.WriteLine(message);
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i+1}. {menuOptions[i]}");
                    Console.ResetColor();
                    
                }
                else
                {
                    Console.WriteLine($"{i+1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length -1) ? 0 : selectedIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    // Handle selection
                    if (selectedIndex == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                
                default:
                    break;
            }
        }
    }
}