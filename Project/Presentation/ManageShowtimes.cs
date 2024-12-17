using System.Runtime.InteropServices;

public class ManageShowtimes
{
    public static void AddSingleShowTimeMenu()
    {
        //confirm to continue
        PresentationHelper.ClearConsole();
        while (true)
        {
            System.Console.WriteLine("you have chosen to add a single screening.");
            if (PresentationHelper.Continue(AdminLogin.AdminMenu))
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
        while (true)
        {
            System.Console.WriteLine("Would you like to see all current movies? (Y/N)");
            int ShowAllMoviesInput = PresentationHelper.AlteredContinue();
            if ( ShowAllMoviesInput == 1)
            {
                Console.Clear();
                System.Console.WriteLine("All movies:");
                System.Console.WriteLine("------------------------");
                System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
                System.Console.WriteLine("------------------------");
                break;
            }
            else if (ShowAllMoviesInput == 0)
            {
                Console.Clear();
                break;
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
            int AboveCorrectInput = PresentationHelper.AlteredContinue();
            if ( AboveCorrectInput == 1)
            {
                if (ShowtimesLogic.AddShowtime(new ShowtimeModel(ShowtimesLogic.GetNextId(), MoviesLogic.GetMovieByName(movieName).Id, datetime, hallId, HallsLogic.GetHallLayout(hallId))) == false)
                {
                    System.Console.WriteLine($"screening could not be added, there is already a screening at {datetime} in hall {hallId}");
                    System.Console.WriteLine("Press anything to go back to the admin menu.");
                    PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
                }
                Console.Clear();
                System.Console.WriteLine("screening was succesfully added.");
                break;
            }
            else if ( AboveCorrectInput == 0)
            {
                Console.Clear();
                System.Console.WriteLine("Press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
        }
        Console.Clear();
        System.Console.WriteLine("Press anything to go back to the menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void AddShowTimesMenu()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            System.Console.WriteLine("you have chosen to add a single screening.");
            if (PresentationHelper.Continue(AdminLogin.AdminMenu))
            {
                break;
            }
            else
            {
                continue;
            }
        }

        Console.Clear();

        //moviename input
        while (true)
        {
            System.Console.WriteLine("Would you like to see all current movies? (Y/N)");
            int SeeAllMoviesInput = PresentationHelper.AlteredContinue();
            if (SeeAllMoviesInput == 1)
            {
                Console.Clear();
                System.Console.WriteLine("All movies:");
                System.Console.WriteLine("------------------------");
                System.Console.WriteLine(MoviesLogic.ShowAllMovieNames());
                System.Console.WriteLine("------------------------");
                break;
            }
            else if (SeeAllMoviesInput == 0)
            {
                Console.Clear();
                break;
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
                int AddMovieInput = PresentationHelper.AlteredContinue();
                if (AddMovieInput == 1)
                {
                    ManageMovies.AddMovieMenu();
                }
                else if (AddMovieInput == 0)
                {
                    System.Console.WriteLine("Chosen not to add a new movie, press anything to go back to the menu.");
                    PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
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
                int check1 = PresentationHelper.AlteredContinue();
                if (check1 == 1)
                {
                    break;
                }
                else if (check1 == 0)
                {
                    AddShowTimesMenu();
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
            int input2 = PresentationHelper.AlteredContinue();
            if (input2 == 1)
            {
                Console.Clear();
                List<DateTime> datetimes = ShowtimesLogic.GenerateDateTimesList(startdate, enddate, time, interval);
                List<ShowtimeModel> validshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).ValidShowtimes;
                List<ShowtimeModel> invalidshowtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes).InvalidShowtimes;
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
                while (true)
                {
                    Console.Clear();
                    System.Console.WriteLine("Add the valid screen times? (Y/N)");
                    if (PresentationHelper.Continue(AdminLogin.AdminMenu))
                    {
                        ShowtimesLogic.AddShowTimes(validshowtimes);
                        System.Console.WriteLine("Successfully added valid screenings.");
                        break;
                    }
                }
                System.Console.WriteLine("Press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);

            }
            else if (input2 == 0)
            {
                Console.Clear();
                System.Console.WriteLine("Press anything to go back to the menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
        }
    }
}