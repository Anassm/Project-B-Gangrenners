public static class MovieSearch
{
    public static void SearchByDate()
    {
        DateTime selectedDate = SelectDate();
        MovieModel selectedMovie = SelectMovie(selectedDate);
        if (selectedMovie == null)
        {
            PresentationHelper.PrintRed("No movie selected");
            PresentationHelper.PrintRed("Going back to main menu");
            Menu.MainMenu();
        }

        string StartMessage = $"You have chosen the following movie:\n{selectedMovie.ToStringUsers()}\n\nDo you want to buy a ticket for this movie?";
        bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
        if (YesNo)
        {
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Menu.MainMenu();
        }

        while (true)
        {
            PresentationHelper.PrintYellow("Please enter the number of tickets you want to buy");
            string ticketsInput = Console.ReadLine();
            List<ShowtimeModel> showtimes = ShowtimesLogic.GetShowtimesByDay(selectedDate);
            int maxSeats = 0;
            foreach (ShowtimeModel showtime in showtimes)
            {
                if (showtime.MoviesId == selectedMovie.Id)
                {
                    if (ShowtimesLogic.CheckAvailability(showtime) >= maxSeats)
                    {
                        maxSeats = ShowtimesLogic.CheckAvailability(showtime);
                    }
                }
            }
            if (int.TryParse(ticketsInput, out int tickets) && tickets > 0)
            {
                if (tickets > maxSeats)
                {
                    string StartMessage3 = $"There are only {maxSeats} seats available for this movie\nWould you like to buy tickets for the available seats?";
                    bool YesNo3 = SelectingMenu.YesNoSelect(StartMessage3);
                    if (YesNo3)
                    {
                        tickets = maxSeats;
                    }
                    else
                    {
                        Menu.MainMenu();
                    }
                }
                BuyTicket.Start(ChooseMovie.StartMovie(selectedMovie, tickets, selectedDate));
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
                continue;
            }
        }

    }

    public static DateTime SelectDate()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintYellow("Select one of the following dates:");
        List<DateTime> dates = Enumerable.Range(0, 15).Select(offset => DateTime.Now.Date.AddDays(offset)).ToList();
        for (int i = 0; i < dates.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dates[i].ToShortDateString()}");
        }

        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index > 0 && index <= dates.Count)
            {
                return dates[index - 1];
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    public static MovieModel SelectMovie(DateTime selectedDate)
    {
        List<MovieModel> movies = MoviesLogic.GetMovies(selectedDate);
        if (movies.Count == 0)
        {
            string StartMessage = "No movies found for this date, would you like to select another date?";
            bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
            if (YesNo)
            {
                Menu.MainMenu();
            }
            else
            {
                
                DateTime newDate = SelectDate();
                SelectMovie(newDate);
            }
        }
        else
        {
            Console.Clear();

        }

        while (true)
        {
            Console.WriteLine($"Movies for {selectedDate.ToShortDateString()}");
            Console.WriteLine(MoviesLogic.DisplayMovies(selectedDate));
            PresentationHelper.PrintYellow("Enter the name of the movie you want to see");
            string movieName = Console.ReadLine();
            MovieModel selectedMovie = MoviesLogic.GetMovieByName(movieName, MoviesLogic.GetMovies(selectedDate));
            if (selectedMovie == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Movie not found");
                Console.ResetColor();
                continue;
            }

            string StartMessage = $"You selected the following movie:\n{selectedMovie.ToStringOneLine()}\nIs this the movie you want to see?";
            bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
            if (YesNo)
            {
                Console.Clear();
                return selectedMovie;
            }
            else
            {
                Console.Clear();
            }
        }
    }

    public static void SearchAll()
    {
        PresentationHelper.ClearConsole();
        Console.WriteLine("All movies for upcoming 14 days");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(MoviesLogic.DisplayMovies(DateTime.Now, DateTime.Now.AddDays(14), true));
        PresentationHelper.PrintYellow("Enter the name of the movie you want to see");
        string movieName = Console.ReadLine();
        MovieModel selectedMovie = MoviesLogic.GetMovieByName(movieName, MoviesLogic.GetMovies(DateTime.Now, DateTime.Now.AddDays(14)));
        if (selectedMovie == null)
        {
            PresentationHelper.PrintRed("Movie not found");
            PresentationHelper.PrintYellow("Press any key to go back to main menu");
            PresentationHelper.PressAnyToContinue(Menu.MainMenu);
        }

        string StartMessage = $"You have chosen the following movie:\n{selectedMovie.ToStringUsers()}\n\nDo you want to buy a ticket for this movie?";
        bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
        if (YesNo)
        {
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Menu.MainMenu();
        }

        while (true)
        {
            PresentationHelper.PrintYellow("Please enter the number of tickets you want to buy");
            string ticketsInput = Console.ReadLine();
            List<ShowtimeModel> showtimes = ShowtimesLogic.GetShowtimesByDay(DateTime.Now, DateTime.Now.AddDays(14));
            int maxSeats = 0;
            foreach (ShowtimeModel showtime in showtimes)
            {
                if (showtime.MoviesId == selectedMovie.Id)
                {
                    if (ShowtimesLogic.CheckAvailability(showtime) >= maxSeats)
                    {
                        maxSeats = ShowtimesLogic.CheckAvailability(showtime);
                    }
                }
            }
            if (int.TryParse(ticketsInput, out int tickets) && tickets > 0)
            {
                if (tickets > maxSeats)
                {

                    string StartMessage2 = $"There are only {maxSeats} seats available for this movie\nWould you like to buy tickets for the available seats?";
                    bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
                    if (YesNo2)
                    {
                        tickets = maxSeats;
                    }
                    else
                    {
                        Menu.MainMenu();
                    }
                }
                BuyTicket.Start(ChooseMovie.StartMovie(selectedMovie, tickets, DateTime.Now, DateTime.Now.AddDays(14)));
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
                continue;
            }
        }

    }
}