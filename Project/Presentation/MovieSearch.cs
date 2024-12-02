public static class MovieSearch
{
    public static void SearchByDate()
    {
        DateTime selectedDate = SelectDate();
        MovieModel selectedMovie = SelectMovie(selectedDate);
        if (selectedMovie == null)
        {
            Console.WriteLine("No movie selected");
            Console.WriteLine("Going back to main menu");
            Menu.MainMenu();
        }

        Console.WriteLine("Please enter the number of tickets you want to buy");
        string ticketsInput = Console.ReadLine();
        List<ShowtimeModel> showtimes = ShowtimesLogic.GetShowtimesByDay(selectedDate);
        int maxSeats = 0;
        foreach(ShowtimeModel showtime in showtimes)
        {
            if(showtime.MoviesId == selectedMovie.Id)
            {
                if(ShowtimesLogic.CheckAvailability(showtime) >= maxSeats)
                {
                    maxSeats = ShowtimesLogic.CheckAvailability(showtime);
                }
            }
        }
        if(int.TryParse(ticketsInput, out int tickets) && tickets > 0)
        {
            if(tickets > maxSeats)
            {
                Console.WriteLine($"There are only {maxSeats} seats available for this movie");
                Console.WriteLine("Would you like to buy tickets for the available seats? ([y]es/[n]o)");
                string response = Console.ReadLine();
                if(response.ToLower() == "y")
                {
                    tickets = maxSeats;
                }
                else
                {
                    Console.WriteLine("Exiting search.");
                    Menu.MainMenu();
                }
            }
            BuyTicket.Start(ChooseMovie.StartMovie(selectedMovie, tickets, selectedDate));
        }
        else
        {
            Console.WriteLine("Invalid input");
            SearchByDate();
        }
    }

    public static DateTime SelectDate()
    {
        Console.WriteLine("Select one of the following dates:");
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
        if(movies.Count == 0)
        {
            Console.WriteLine("No movies found for this date");
            Console.WriteLine("Would you like to select another date? ([y]es/[n]o)");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                DateTime newDate = SelectDate();
                SelectMovie(newDate);
            }
            else
            {
                Console.WriteLine("Exiting search.");
                Menu.MainMenu();
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
            Console.WriteLine("Enter the name of the movie you want to see");
            string movieName = Console.ReadLine();
            MovieModel selectedMovie = MoviesLogic.GetMovieByName(movieName, MoviesLogic.GetMovies(selectedDate));
            if(selectedMovie == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Movie not found");
                Console.ResetColor();
                continue;
            }

            Console.WriteLine("You selected the following movie:");
            Console.WriteLine(selectedMovie.ToStringOneLine());
            Console.WriteLine("Is this the movie you want to see? ([y]es/[n]o)");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                Console.Clear();
                return selectedMovie;
            }
            Console.Clear();

        }
    }
    
    public static void SearchAll()
    {

    }


}