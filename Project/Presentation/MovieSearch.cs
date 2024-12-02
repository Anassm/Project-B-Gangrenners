public static class MovieSearch
{
    public static void SearchByDate()
    {
        Console.WriteLine("Select one of the following dates:");
        List<DateTime> dates = Enumerable.Range(0, 15).Select(offset => DateTime.Now.Date.AddDays(offset)).ToList();
        for (int i = 0; i < dates.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dates[i].ToShortDateString()}");
        }

        // Display the movies for the selected date
        string input = Console.ReadLine();
        DateTime selectedDate = DateTime.Now;
        if(int.TryParse(input, out int index) && index > 0 && index <= dates.Count)
        {
            selectedDate = dates[index - 1];
            List<MovieModel> movies = MoviesLogic.GetMovies(selectedDate);
            if(movies.Count == 0)
            {
                Console.WriteLine("No movies found for this date");
            }
            else
            {
                Console.WriteLine(MoviesLogic.DisplayMovies(selectedDate));
            }
        }
        else
        {
            Console.WriteLine("Invalid input");
            SearchByDate();
        }

        // Give the user the option to select a movie
        Console.WriteLine("Enter the name of the movie you want to see");
        string movieName = Console.ReadLine();
        MovieModel selectedMovie = MoviesLogic.GetMovieByName(movieName);
        if(selectedMovie == null)
        {
            Console.WriteLine("Movie not found");
            SearchByDate();
        }
        else
        {
            Console.WriteLine("You selected the following movie:");
            Console.WriteLine(selectedMovie.Name);
            Console.WriteLine("Please enter the number of tickets you want to buy");
            string ticketsInput = Console.ReadLine();
            if(int.TryParse(ticketsInput, out int tickets) && tickets > 0)
            {
                BuyTicket.Start(ChooseMovie.StartMovie(selectedMovie, tickets, selectedDate));
            }
            else
            {
                Console.WriteLine("Invalid input");
                SearchByDate();
            }
        }
    }

    public static void SearchAll()
    {

    }


}