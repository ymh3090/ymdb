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
        // Global repository instance so all methods can access it
        static MovieRepository repository = new MovieRepository();

        static void Main()
        {
            showlogo();
            while (true)
            {
                int choice = ShowMenu();

                switch (choice)
                {
                    case 1: Add(); break;
                    case 2: Updatetowatched(repository); break;
                    case 3: Delete(repository); break;
                    case 4: SearchByYear(repository); break;
                    case 5: SearchByTitle(repository); break;
                    case 6: ListMoviesByYear(repository); break;
                    case 7: ListMoviesByTitle(repository); break;
                    case 8: EXITprogram(); break;

                }
            }
        }

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

        static int ShowMenu()
        {
            while (true)
            {
                Console.WriteLine(@"
        [1] Add a movie
        [2] Mark a movie as watched
        [3] Delete a movie from watchlist
        [4] Search movies by year
        [5] Search movies by title
        [6] List all movies sorted by year
        [7] List all movies sorted by title
        [8] Exit
        ");

                Console.Write("Choose an option (1-8): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 8)
                    return choice;

                Console.Clear();
                Console.WriteLine("Invalid input. Please choose a number (eg.1,2) from 1-8.");
            }
        }


















        static void Add()
        {
            try
            {

                Console.Write("\nEnter the title: ");
                string? title = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(title) || title.Length > 40)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Title cannot be empty and must be at most 40 characters.");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nEnter the title: ");
                    title = Console.ReadLine();
                }




                Console.Write("Enter the director name: ");
                string? director = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(director) || director.Length > 40)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("director name cannot be empty and must be at most 20 characters.");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nEnter the director name: ");
                    director = Console.ReadLine();
                }

                int releaseYear;
                while (true)
                {
                    Console.Write("Enter the release year (4-digit): ");
                    string? yearInput = Console.ReadLine();

                    if (int.TryParse(yearInput, out releaseYear) && releaseYear >= 1850 && releaseYear <= 2026)
                    {
                        break;
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Release year must be a valid 4-digit number.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }



                Console.Clear();


                MovieStatus status;
                while (true)
                {
                    Console.WriteLine("Choose the status of the movie:");
                    Console.WriteLine("1) Watched");
                    Console.WriteLine("2) Watchlisted");
                    Console.Write("Enter your choice (1 or 2): ");
                    string? statusInput = Console.ReadLine();

                    if (statusInput == "1")
                    {
                        status = MovieStatus.Watched;
                        break;
                    }
                    else if (statusInput == "2")
                    {
                        status = MovieStatus.Watchlisted;
                        break;
                    }
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }


                Console.Clear();

                string genre;
                while (true)
                {
                    Console.WriteLine("Choose the genre of the movie:");
                    Console.WriteLine("[1] Action    [2] Comedy   [3] Drama    [4] Horror   [5] Sci-Fi   [6] Romance  [7] Thriller");
                    Console.Write("\nEnter your choice (1-7): ");
                    string? Input = Console.ReadLine();

                    switch (Input)
                    {
                        case "1": genre = "Action"; break;
                        case "2": genre = "Comedy"; break;
                        case "3": genre = "Drama"; break;
                        case "4": genre = "Horror"; break;
                        case "5": genre = "Sci-Fi"; break;
                        case "6": genre = "Romance"; break;
                        case "7": genre = "Thriller"; break;
                        default:
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice. Please enter a single number between 1 and 7.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            continue;
                    }

                    break;

                }


                Movie newMovie = new Movie(title, director, releaseYear, status, genre);
                repository.Add(newMovie); // Save to JSON
                Console.WriteLine("\nMovie added !");

            }
            catch (Exception ex)
            {
                Console.WriteLine(" An error occurred while adding the movie: " + ex.Message);
            }


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














        static void Delete(MovieRepository repo)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the title of the movie you want to delete from your watchlist:");
                    string? title = Console.ReadLine();


                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Title cannot be empty. Please try again.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        continue;
                    }

                    bool deleted = repo.DeleteMovie(title);

                    if (deleted)
                    {
                        Console.WriteLine($"'{title}' has been deleted from your watchlist.");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Movie '{title}' was not found in your watchlist.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine("Do you want to try again? (y/n):");
                        string? retry = Console.ReadLine();

                        if (retry?.Trim().ToLower() != "y")
                        {
                            break; // Exit if user doesn't want to retry
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while deleting the movie: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }

















        static void Updatetowatched(MovieRepository repo)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the title of the movie you want to delete from your watchlist:");
                    string? title = Console.ReadLine();


                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Title cannot be empty. Please try again.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        continue;
                    }

                    bool updated = repo.UpdateToWatched(title);

                    if (updated)
                    {
                        Console.Clear();
                        Console.WriteLine($"'{title}' has been marked as Watched");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Movie '{title}' was not found in your watchlist.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine("Do you want to try again? (Y/N):");
                        string? retry = Console.ReadLine();

                        if (retry?.Trim().ToLower() != "y")
                        {
                            break; // Exit if user doesn't want to retry
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while updating the movie: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }
















        static void ListMoviesByTitle(MovieRepository repo)
        {
            try
            {
                var movies = repo.SortByTitle();

                if (movies.Count == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No movies in your watchlist.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return;
                }

                Console.WriteLine("\nMovies sorted by Title:\n");

                foreach (var movie in movies)
                {
                    Console.WriteLine(movie.print());
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error listing movies: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                return;
            }
        }


























        static void ListMoviesByYear(MovieRepository repo)
        {
            try
            {
                var movies = repo.SortByYear(); // Sorted list from repository

                if (movies.Count == 0)
                {
                    Console.WriteLine("No movies in your database.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nMovies sorted by Year:\n");
                Console.ResetColor();

                foreach (var movie in movies)
                {
                    Console.WriteLine(movie.print()); // Calls Print() method from Movie.cs
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error listing movies: {ex.Message}");
            }
        }














        static void SearchByYear(MovieRepository repo)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the release year to search (or type 'back' to return):");
                    string? input = Console.ReadLine();

                    if (input?.Trim().ToLower() == "back")
                        break;

                    if (int.TryParse(input, out int year) && year >= 1850 && year <= 2026)
                    {
                        var results = repo.SearchByYear(year);

                        if (results.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No movies found for year {input}");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.WriteLine($"\nMovies from {year}:\n");

                            foreach (var movie in results)
                            {
                                Console.WriteLine(movie.print());
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid year. Please enter a valid 4-digit number.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error searching by year: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }






















        static void SearchByTitle(MovieRepository repo)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the title to search (or type 'back' to return):");
                    string? title = Console.ReadLine();

                    if (title?.Trim().ToLower() == "back")
                        break;

                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Title cannot be empty. Please try again.");
                        continue;
                    }

                    var results = repo.SearchByTitle(title);

                    if (results.Count == 0)
                    {
                        Console.WriteLine($"No movies found with title containing '{title}'.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\nMovies matching '{title}':\n");
                        foreach (var movie in results)
                        {
                            Console.WriteLine(movie.print());
                        }
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error searching by title: {ex.Message}");

                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }



















        static void EXITprogram()
        {
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

        }
    }

}






// https://www.geeksforgeeks.org/c-sharp/c-sharp-list-class/