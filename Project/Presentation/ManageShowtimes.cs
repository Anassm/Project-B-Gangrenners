using System.Runtime.InteropServices;

public class ManageShowtimes
{
    public static void AddSingleShowTimeMenu()
    {
        //confirm to continue
        PresentationHelper.ClearConsole();
        while (true)
        {
            string StartMessage2 = "you have chosen to add a single screening.";
            bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
            if (YesNo2)
            {
                break;
            }
            else
            {
                continue;
            }
        }

        Console.Clear();
        //show all movies
        string StartMessage = "Would you like to see all current movies?";
        bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
        if (YesNo)
        {
            Console.Clear();
            System.Console.WriteLine("All movies:");
            System.Console.WriteLine("------------------------");
            System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
            System.Console.WriteLine("------------------------");
        }
        else
        {
            Console.Clear();
        }

        //Movie Name
        string movieName = "";
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the name of the movie:");
            movieName = Console.ReadLine();
            if (MoviesLogic.GetMovieByName(movieName) == null)
            {
                System.Console.WriteLine("Please enter an existing movie");
                Console.Clear();
                continue;
            }
            else
            {
                break;
            }
        }

        //date
        DateOnly date = DateOnly.Parse("2024-12-12");
        TimeOnly time = TimeOnly.Parse("00:00");
        DateTime datetime = date.ToDateTime(time);
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the date of the showing (DD-MM-YYYY):");
            string dateString = Console.ReadLine();
            if (MoviesLogic.IsValidDateFormat(dateString))
            {
                date = DateOnly.Parse(dateString);
                if (date > today)
                {
                    break;
                }
                else
                {
                    System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                    continue;
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid format.");
                continue;
            }
        }

        //time
        string playtime = "";
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("please give the time when the movie is played (HH:MM):");
            playtime = Console.ReadLine();
            if (MoviesLogic.IsValidTimeOnlyFormat(playtime))
            {
                time = TimeOnly.Parse(playtime);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid format.");
                continue;
            }
            datetime = date.ToDateTime(time);
            if (DateTime.Now > datetime)
            {
                Console.Clear();
                System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                continue;
            }
            else
            {
                break;
            }
        }

        //hall id
        int hallId = 0;
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the hall ID:");
            string hallIdString = Console.ReadLine();
            System.Console.WriteLine("");
            if (hallIdString.All(char.IsDigit))
            {
                hallId = Convert.ToInt32(hallIdString);
                if (hallId > 0 && hallId <= 3)
                {
                    break;
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        string data = $"---------------------------------------\nMovie name: {movieName}\nDate and time: {datetime}\nHall ID: {hallId}\n---------------------------------------\n\nis the above correct?";


        bool ConfirmInput = SelectingMenu.YesNoSelect(data);
        if (ConfirmInput)
        {
            if (ShowtimesLogic.AddShowtime(new ShowtimeModel(ShowtimesLogic.GetNextId(), MoviesLogic.GetMovieByName(movieName).Id, datetime, hallId, HallsLogic.GetHallLayout(hallId))) == false)
            {
                System.Console.WriteLine($"screening could not be added, there is already a screening at {datetime} in hall {hallId}");
                System.Console.WriteLine("Press anything to go back to the menu.");
                ConsoleKeyInfo key2 = Console.ReadKey(true);
                if (key2.Key != null)
                {
                    Console.Clear();
                    AdminLogin.AdminMenu();
                }
            }
            Console.Clear();
            System.Console.WriteLine("screening was succesfully added.");
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("Press anything to go back to the menu.");
            ConsoleKeyInfo key2 = Console.ReadKey(true);
            if (key2.Key != null)
            {
                Console.Clear();
                AdminLogin.AdminMenu();
            }
        }

        Console.Clear();
        PresentationHelper.PrintYellow("Press anything to go back to the menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }
    public static List<ShowtimeModel> invalidshowtimes = [];
    public static List<ShowtimeModel> validshowtimes = [];
    public static List<DateTime> datetimes = [];

    public static void AddShowTimesMenu()
    {
        //confirm to continue
        PresentationHelper.ClearConsole();
        string StartMessage4 = "you have chosen to add multiple screenings.\nDo you want to continue?";
        bool YesNo4 = SelectingMenu.YesNoSelect(StartMessage4);
        if (!YesNo4)
        {
            AdminLogin.AdminMenu();
        }
        else
        {
            PresentationHelper.ClearConsole();

        }

        //moviename input
        string StartMessage = "Would you like to see all current movies?";
        bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
        if (YesNo)
        {
            Console.Clear();
            System.Console.WriteLine("All movies:");
            System.Console.WriteLine("------------------------");
            System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
            System.Console.WriteLine("------------------------");
        }
        else
        {
            Console.Clear();
        }

        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Please give the name of the movie:");
        string movieName = Console.ReadLine();
        if (MoviesLogic.GetMovieByName(movieName) == null)
        {
            string StartMessage2 = "this movie does not exist, would you like to add the movie?";
            bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
            if (YesNo2)
            {
                ManageMovies.AddMovieMenu();
            }
            else
            {
                PresentationHelper.PrintYellow("Chosen not to add a new movie, press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
        }

        //start date input
        DateOnly startdate = DateOnly.Parse("2024-12-12");
        DateOnly enddate = DateOnly.Parse("2024-12-12");
        TimeOnly time = TimeOnly.Parse("00:00");
        DateTime datetime = startdate.ToDateTime(time);
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        while (true)
        {
            //start date
            while (true)
            {
                System.Console.WriteLine("");
                PresentationHelper.PrintYellow("Please give the date of the first showing (DD-MM-YYYY):");
                string dateString = Console.ReadLine();
                if (MoviesLogic.IsValidDateFormat(dateString))
                {
                    startdate = DateOnly.Parse(dateString);
                    if (startdate > today)
                    {
                        break;
                    }
                    else
                    {
                        System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    System.Console.WriteLine("Invalid format.");
                    continue;
                }
            }


            //screen time
            while (true)
            {
                System.Console.WriteLine("");
                PresentationHelper.PrintYellow("please give the time when the movie is played (HH:MM):");
                string playtime = Console.ReadLine();
                if (MoviesLogic.IsValidTimeOnlyFormat(playtime))
                {
                    time = TimeOnly.Parse(playtime);
                }
                else
                {
                    Console.Clear();
                    System.Console.WriteLine("Invalid format.");
                    continue;
                }
                datetime = startdate.ToDateTime(time);
                if (DateTime.Now > datetime)
                {
                    Console.Clear();
                    System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                    continue;
                }
                else
                {
                    break;
                }
            }
            Console.Clear();

            //confirm
            string StartMessage2 = $"Start date and time: {datetime}\nIs this correct?";
            bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
            if (YesNo2)
            {
                break;
            }
            else
            {
                AddShowTimesMenu();
            }
            break;
        }

        //end date
        while (true)
        {

            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the date you would like the showings to end (DD-MM-YYYY):");
            string dateString2 = Console.ReadLine();
            if (MoviesLogic.IsValidDateFormat(dateString2))
            {
                enddate = DateOnly.Parse(dateString2);
                if (enddate > today && enddate > startdate)
                {
                    break;
                }
                else
                {
                    System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                    continue;
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid format.");
                continue;
            }
        }


        //interval input
        int interval = 0;
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the amount of days between each showing:");
            string intervalstring = Console.ReadLine();
            System.Console.WriteLine("");
            if (intervalstring.All(char.IsDigit))
            {
                interval = Convert.ToInt32(intervalstring);
                if (interval > 0 && interval < (enddate.DayNumber - startdate.DayNumber))
                {

                }
                else
                {

                }
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        //hall ID
        int hallId = 0;
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the hall ID:");
            string hallIdString = Console.ReadLine();
            System.Console.WriteLine("");
            if (hallIdString.All(char.IsDigit))
            {
                hallId = Convert.ToInt32(hallIdString);
                if (hallId > 0 && hallId <= 3)
                {
                    break;
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        //confirm all data
        while (true)
        {

            string StartMessage2 =
                $"---------------------------------------" +
                $"\nMovie name: {movieName}" +
                $"\nStart date: {startdate}" +
                $"\nEnd date: {enddate}" +
                $"\nDays between showings: {interval}" +
                $"\nTime of showing: {time}" +
                $"\nHall ID: {hallId}" +
                $"\n---------------------------------------" +
                $"\nis the above correct?";
            bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
            if (YesNo2)
            {
                Console.Clear();
                datetimes = ShowtimesLogic.GenerateDateTimesList(startdate, enddate, time, interval);
                validshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).ValidShowtimes;
                invalidshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).InvalidShowtimes;
                // System.Console.WriteLine("Valid screen times:");
                // System.Console.WriteLine("---------------------------------------");
                // foreach (ShowtimeModel showtime in validshowtimes)
                // {
                //     System.Console.WriteLine($"Date: {showtime.Time}");
                //     System.Console.WriteLine("---------------------------------------");
                // }
                // System.Console.WriteLine("");
                // System.Console.WriteLine("Invalid screen times:");
                // System.Console.WriteLine("---------------------------------------");
                // foreach (ShowtimeModel showtime in invalidshowtimes)
                // {
                //     System.Console.WriteLine($"Date: {showtime.Time}");
                //     System.Console.WriteLine("---------------------------------------");
                // }
                // string StartMessage3 = "Add the valid screen times?";
                bool YesNo3 = SelectingMenu.YesNoSelect(Print);
                if (YesNo3)
                {
                    ShowtimesLogic.AddShowTimes(validshowtimes);
                    PresentationHelper.PrintGreen("Successfully added valid screenings.");
                    break;
                }
                PresentationHelper.PrintYellow("Press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
            else
            {
                Console.Clear();
                PresentationHelper.PrintYellow("Press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
        }
    }

    public static void Print()
    {
        System.Console.WriteLine("Valid screen times:");
        System.Console.WriteLine("---------------------------------------");
        foreach (ShowtimeModel showtime in validshowtimes)
        {
            System.Console.WriteLine($"Date: {showtime.Time}");
            System.Console.WriteLine("---------------------------------------");
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("Invalid screen times:");
        System.Console.WriteLine("---------------------------------------");
        foreach (ShowtimeModel showtime in invalidshowtimes)
        {
            System.Console.WriteLine($"Date: {showtime.Time}");
            System.Console.WriteLine("---------------------------------------");
        }
        System.Console.WriteLine();
        System.Console.WriteLine("Add the valid screen times?");
    }
}
