using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters;
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
    internal class Program
    {
        static void showlogo()
        {
            string text = @"                                 _______________________________________
                                |                                       |    
                                | Welcome to Your Movie manager YMDB    |
                                |_______________________________________|";
            int conswidth = Console.WindowWidth;

            Console.ForegroundColor = ConsoleColor.Cyan;
            int padding = (conswidth - text.Length) / 2;

            padding = Math.Max(0, padding);

            Console.WriteLine("{0," + (padding + text.Length) + "}", text);

        }

        static MovieRepository repository = new MovieRepository();

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
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Thank you for using YMDB! Goodbye!");
                        Console.Write("Exiting program");
                        for (int i = 0; i < 5; i++)
                        {
                            Thread.Sleep(500);
                            Console.Write(".");
                        }
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static int ShowMenu()
        {
            while (true){
            

                Console.WriteLine(@"
        [1] Add a movie you've watched / you want to watch it later
        [2] Delete a movie from watchlist
        [3] Update a movie (if you watched it)
        [4] Search movies by year
        [5] Search movies by title
        [6] Exit
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
            try
            {
                Console.WriteLine("Add movie to your watchlist or you have watched it.");

                Console.Write("Enter the title: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title) || !title.Any(char.IsLetter))
                {
                    Console.Clear();
                    Console.WriteLine("Title msut contain letters.");
                    return;
                }

                Console.Write("Enter the director: ");
                string director = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(director))
                {
                    Console.Clear();
                    Console.WriteLine("Director name cannot be empty.");
                    return;
                }

                Console.Write("Enter the release year (4-digit): ");
                if (!int.TryParse(Console.ReadLine(), out int releaseYear) || releaseYear < 1850 || releaseYear > DateTime.Now.Year + 1)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid release year. Must be a 4-digit number.");
                    return;
                }

                Console.Clear();
                Console.WriteLine("Choose the status of the movie:");
                Console.WriteLine("1) Watched");
                Console.WriteLine("2) Watchlisted");
                Console.Write("Enter your choice (1 or 2): ");
                string statusInput = Console.ReadLine();
                MovieStatus status = statusInput == "1" ? MovieStatus.Watched : MovieStatus.Watchlisted;

                Console.Clear();
                Console.WriteLine("Choose the genre of the movie:");
                Console.WriteLine("[1] Action    [2] Comedy   [3] Drama    [4] Horror   [5] Sci-Fi   [6] Romance  [7] Thriller");
                Console.Write("\nEnter your choice (1-7): ");

                string[] genres = { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Romance", "Thriller" };
                string Input = Console.ReadLine();
                int genreIndex = 0;

                if (!int.TryParse(Input, out genreIndex) || genreIndex < 1 || genreIndex > genres.Length)
                {
                    Console.WriteLine("Invalid genre choice. Defaulting to 'Unknown'.");
                    genreIndex = 0;
                }

                string genre = genreIndex > 0 ? genres[genreIndex - 1] : "Unknown";

                Movie movie = new Movie(title, director, releaseYear, status, genre);
                repository.Add(movie);

                Console.WriteLine("\n Movie added successfully!");
                Console.WriteLine(movie.print());

            }
            catch (Exception ex)
            {
                Console.WriteLine(" An error occurred while adding the movie: " + ex.Message);
            }
            Console.Clear();
            
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