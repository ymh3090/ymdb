using System.Transactions;
using System.Xml.Serialization;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Text;
/*

 Each movie must have a title, director name, release year, and genre.
 Let the user add, delete, update, and search movies by title or year.
 Make sure the release year is a 4-digit number, and the title/director are nonempty strings.
 Add an option to list all movies sorted by year or title. 

*/

namespace ymdb
{

    public enum MovieStatus
    {
        Watched,
        Watchlisted
    }

    public class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public MovieStatus Status { get; set; }
        public string genre { get; set; }

        public Movie(string title, string director, int releaseYear, MovieStatus status)
        {
            /*
            
            
            if (releaseYear < 1000 || releaseYear > 9999)
                throw new ArgumentException("Release year must be a 4-digit number.");
            if(title.Length > 20)
                throw new ArgumentException("Title cannot exceed 100 characters.");
            if(director.Length > 20)
                throw new ArgumentException("Director name cannot exceed 100 characters.");
            */

            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Status = status;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Director: {Director}, Year: {ReleaseYear}, Status: {Status}";
        }
    }


    internal class Program
    {
        static void showlogo()
        {
            string text = "Welcome to Your Movie manager YMDB";
            int conswidth = Console.WindowWidth;

            Console.ForegroundColor = ConsoleColor.Cyan;
            int padding = (conswidth - text.Length) / 2;

            padding = Math.Max(0, padding);

            Console.WriteLine("{0," + (padding + text.Length) + "}", text);

        }

        static void Main()
        {
            showlogo();
            while (true)
            {
                int choice = ShowMenu();

                switch (choice)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Deletefromwatchlist();
                        break;
                    case 3:
                        Updatetowatched();
                        break;
                    case 4:
                        SearchByYear();
                        break;
                    case 5:
                        SearchByTitle();
                        break;
                    case 6:
                    Console.Write("Exiting program");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(500);    // wait 0.5 seconds
                        Console.Write("."); 
                    }
                    Environment.Exit(0);
                    break;
                }
            }
        }

        static int ShowMenu()
        {
            while (true)
            {
                Console.WriteLine(@"
                    1) Add a movie you've watched / you want to watch it later
                    2) Delete a movie from watchlist
                    3) Update a movie (if you watched it)
                    4) Search movies by year
                    5) Search movies by title
                    6) Exit
                    "
                );

                Console.Write("Choose an option (1-6): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice >= 1 && choice <= 6)
                    {
                        return choice;
                    }
                }
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            }
        }






        static void Add()
        {
            Console.WriteLine("Add movie to your watchlist or you have watched it.");
            Console.Write("enter the title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (title.Length > 40)
            {
                Console.WriteLine("Title cannot exceed 40 characters.");
                return;
            }

            Console.Write("enter the director: ");
            string director = Console.ReadLine();
            Console.Write("enter the release year (4-digit): ");
            string releaseYear = Console.ReadLine();
            Console.WriteLine("Choose the status of the movie:");
            Console.WriteLine("1) Watched");
            Console.WriteLine("2) Watchlisted");
            Console.Write("Enter your choice (1 or 2): ");
            string statusInput = Console.ReadLine();
            MovieStatus status = statusInput == "1" ? MovieStatus.Watched : MovieStatus.Watchlisted;

            Movie movie = new Movie(title, director, releaseYear.Length == 4 ? int.Parse(releaseYear) : throw new ArgumentException("Release year must be a 4-digit number."), status);
        }







/*
The Shawshank Redemption	            1994	Frank Darabont	    Drama
The Dark Knight	                        2008	Christopher Nolan	Action, Crime
Inception	                            2010 	Christopher Nolan	Sci-Fi, Action
Pulp Fiction	                        1994	Quentin Tarantino	Crime, Drama
Parasite	                            2019	Bong Joon-ho	    Thriller, Dark Comedy
Spirited Away	                        2001	Hayao Miyazaki	    Fantasy, Adventure
Get Out	                                2017    Jordan Peele	    Horror, Thriller
The Godfather	                        1972	Francis Coppola	    Crime, Drama
Mad Max: Fury Road	                    2015	George Miller	    Action, Adventure
Eternal Sunshine of the Spotless Mind	2004	Michel Gondry	    Sci-Fi, Romance
*/





        static void Deletefromwatchlist()
        {

            string title;
            Console.WriteLine("enter the title of the movie you want to delete from your watchlist.");
            title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            Console.WriteLine("enter the title of the movie or enter 1 to go to main menu .");
        }










        static void Updatetowatched()
        {
            string title;
            System.Console.WriteLine("enter the title of the movie you have watched from your watchlist.");
            title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            Console.WriteLine("enter the title of the movie or enter 1 to go to main menu .");

        }











        static void ListMoviesByYear()
        {
            // This method should list all movies sorted by year.
            // Implementation will depend on how you store the movies.
            
        }
        static void SearchByYear()
        {
            Console.WriteLine("Enter the release year to search for movies:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int year) && year >= 1000 && year <= 9999)
            {
                // Search logic here
                // Implementation will depend on how you store the movies.
                Console.WriteLine($"Searching for movies released in {year}...");
            }
            else
            {
                Console.WriteLine("Invalid year. Please enter a 4-digit number.");
            }


        }
        static void SearchByTitle()
        { 
            // This method should search movies by title.
            // Implementation will depend on how you store the movies.
        }

    }
}