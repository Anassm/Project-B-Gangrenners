public static class SelectingMenu
{
    public static void MenusSelect(string[] menuOptions, Action[] menuActions, string StartMessage)
    {
        int selectedIndex = 0;

        while (true)
        {
            PresentationHelper.ClearConsole();
            PresentationHelper.PrintYellow(StartMessage);

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
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
        String[] menuOptions = { "Yes", "No" };

        while (true)
        {
            PresentationHelper.ClearConsole();
            PresentationHelper.PrintYellow(message);
            Console.WriteLine();
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
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

    public static bool YesNoSelect(Action action)
    {
        int selectedIndex = 0;
        String[] menuOptions = { "Yes", "No" };

        while (true)
        {
            PresentationHelper.ClearConsole();
            action();
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
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

    public static bool YesNoSelect(string message, string[] menuOptions)
    {
        int selectedIndex = 0;

        while (true)
        {
            System.Console.WriteLine(message);
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
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

    public static void MenusSelectMainMenu(string[] menuOptions, Action[] menuActions)
    {
        int selectedIndex = 0;

        while (true)
        {
            PresentationHelper.ClearConsole();
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("   _____          _   _  _____ ______ _____  ______ _   _ _   _ ______ _____   _____  ");
            System.Console.WriteLine("  / ____|   /\\   | \\ | |/ ____|  ____|  __ \\|  ____| \\ | | \\ | |  ____|  __ \\ / ____|");
            System.Console.WriteLine(" | |  __   /  \\  |  \\| | |  __| |__  | |__) | |__  |  \\| |  \\| | |__  | |__) | (___ ");
            System.Console.WriteLine(" | | |_ | / /\\ \\ | . ` | | |_ |  __| |  _  /|  __| | . ` | . ` |  __| |  _  / \\___ \\");
            System.Console.WriteLine(" | |__| |/ ____ \\| |\\  | |__| | |____| | \\ \\| |____| |\\  | |\\  | |____| | \\ \\ ____) | ");
            System.Console.WriteLine("  \\_____/_/    \\_\\_| \\_|\\_____|______|_|  \\_\\______|_| \\_|_| \\_|______|_|  \\_\\_____/ ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("                                          _      ________   _________  __  _______ __");
            System.Console.WriteLine("                                         | | /| / / __/ /  / ___/ __ \\/  |/  / __// / ");
            System.Console.WriteLine("                                         | |/ |/ / _// /__/ /__/ /_/ / /|_/ / _/ /_/ ");
            System.Console.WriteLine("                                         |__/|__/___/____/\\___/\\____/_/  /_/___/(_)");
            Console.ResetColor();
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
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
}