using System.Runtime.InteropServices;

public class ManageShowtimes
{
    public static void AddSingleShowTimeMenu()
    {
        //confirm to continue
        while (true)
        {
            Console.Clear();
            System.Console.WriteLine("you have chosen to add a single screening.");
            System.Console.WriteLine("Do you want to continue? (Y/N)");
            string input = Console.ReadLine().ToLower();
            if (input == "n" || input == "no")
            {
                Console.Clear();
                AdminLogin.AdminMenu();
            }
            if (input == "y" || input == "yes")
            {
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
                continue;
            }
        }
        Console.Clear();
        //show all movies
        while (true)
        {
            System.Console.WriteLine("Would you like to see all current movies? (Y/N)");
            string showallmovies = Console.ReadLine().ToLower();
            if (showallmovies == "y" || showallmovies == "yes")
            {
                Console.Clear();
                System.Console.WriteLine("All movies:");
                System.Console.WriteLine("------------------------");
                System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
                System.Console.WriteLine("------------------------");
                break;
            }
            else if (showallmovies == "n" || showallmovies == "no")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        //Movie Name
        string movieName = "";
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the name of the movie:");
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
            System.Console.WriteLine("Please give the date of the showing (DD-MM-YYYY):");
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
            System.Console.WriteLine("please give the time when the movie is played (HH:MM):");
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
            System.Console.WriteLine("Please give the hall ID:");
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

        while (true)
        {
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine($"Movie name: {movieName}");
            System.Console.WriteLine($"Date and time: {datetime}");
            System.Console.WriteLine($"Hall ID: {hallId}");
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("is the above correct? (Y/N)");
            string input2 = Console.ReadLine().ToLower();
            if (input2 == "y" || input2 == "yes")
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
                break;
            }
            else if (input2 == "n" || input2 == "no")
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
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input, press anything to try again.");
                ConsoleKeyInfo key3 = Console.ReadKey(true);
                if (key3.Key != null)
                {
                    Console.Clear();
                    continue;
                }
            }
        }
        Console.Clear();
        System.Console.WriteLine("Press anything to go back to the menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }

    public static void AddShowTimesMenu()
    {
        while (true)
        {
            Console.Clear();
            System.Console.WriteLine("you have chosen to add screenings.");
            System.Console.WriteLine("Do you want to continue? (Y/N)");
            string input = Console.ReadLine().ToLower();
            if (input == "n" || input == "no")
            {
                Console.Clear();
                AdminLogin.AdminMenu();
            }
            if (input == "y" || input == "yes")
            {
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
                continue;
            }
        }

        Console.Clear();

        //moviename input
        while (true)
        {
            System.Console.WriteLine("Would you like to see all current movies? (Y/N)");
            string showallmovies = Console.ReadLine().ToLower();
            if (showallmovies == "y" || showallmovies == "yes")
            {
                Console.Clear();
                System.Console.WriteLine("All movies:");
                System.Console.WriteLine("------------------------");
                System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
                System.Console.WriteLine("------------------------");
                break;
            }
            else if (showallmovies == "n" || showallmovies == "no")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("Please give the name of the movie:");
        string movieName = Console.ReadLine();
        if (MoviesLogic.GetMovieByName(movieName) == null)
        {
            while (true)
            {
                System.Console.WriteLine("This movie does not exist, would you like to add the movie? (Y/N)");
                string input1 = Console.ReadLine().ToLower();
                if (input1 == "y" || input1 == "yes")
                {
                    ManageMovies.AddMovieMenu();
                }
                else if (input1 == "n" || input1 == "no")
                {
                    System.Console.WriteLine("Chosen not to add a new movie, press anything to go back to the menu.");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != null)
                    {
                        Console.Clear();
                        AdminLogin.AdminMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    System.Console.WriteLine("Invalid input.");
                }
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
                System.Console.WriteLine("Please give the date of the first showing (DD-MM-YYYY):");
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
                System.Console.WriteLine("please give the time when the movie is played (HH:MM):");
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
            while (true)
            {
                System.Console.WriteLine($"Start date and time: {datetime}");
                System.Console.WriteLine("Is this correct? (Y/N)");
                string check1 = Console.ReadLine().ToLower();
                if (check1 == "y" || check1 == "yes")
                {
                    break;
                }
                else if (check1 == "n" || check1 == "no")
                {
                    AddShowTimesMenu();
                }
                else
                {
                    System.Console.WriteLine("Invalid input.");
                }
            }
            break;
        }

        //end date
        while (true)
        {

            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the date you would like the showings to end (DD-MM-YYYY):");
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
            System.Console.WriteLine("Please give the amount of days between each showing:");
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
            System.Console.WriteLine("Please give the hall ID:");
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
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine($"Movie name: {movieName}");
            System.Console.WriteLine($"Start date: {startdate}");
            System.Console.WriteLine($"End date: {enddate}");
            System.Console.WriteLine($"Days between showings: {interval}");
            System.Console.WriteLine($"Time of showing: {time}");
            System.Console.WriteLine($"Hall ID: {hallId}");
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("is the above correct? (Y/N)");
            string input2 = Console.ReadLine().ToLower();
            if (input2 == "y" || input2 == "yes")
            {
                Console.Clear();
                List<DateTime> datetimes = ShowtimesLogic.GenerateDateTimesList(startdate, enddate, time, interval);
                List<ShowtimeModel> validshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).ValidShowtimes;
                List<ShowtimeModel> invalidshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).InvalidShowtimes;
                ShowtimesLogic.AddShowTimes(validshowtimes);
                System.Console.WriteLine("Valid screen times:");
                System.Console.WriteLine("---------------------------------------");
                foreach (ShowtimeModel showtime in validshowtimes)
                {
                    System.Console.WriteLine(showtime.ToString());
                    System.Console.WriteLine("---------------------------------------");
                }
                System.Console.WriteLine("");
                System.Console.WriteLine("Invalid screen times:");
                System.Console.WriteLine("---------------------------------------");
                foreach (ShowtimeModel showtime in invalidshowtimes)
                {
                    System.Console.WriteLine(showtime.ToString());
                    System.Console.WriteLine("---------------------------------------");
                }
                System.Console.WriteLine("Press anything to go back to the menu.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != null)
                {
                    Console.Clear();
                    AdminLogin.AdminMenu();
                }

            }
            else if (input2 == "n" || input2 == "no")
            {
                Console.Clear();
                System.Console.WriteLine("Press anything to go back to the menu.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != null)
                {
                    Console.Clear();
                    AdminLogin.AdminMenu();
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input, press anything to try again.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != null)
                {
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}