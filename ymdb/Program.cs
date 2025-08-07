using System.Transactions;
using System.Xml.Serialization;
using System.Threading;
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

        public Movie(string title, string director, int releaseYear, MovieStatus status)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(director))
                throw new ArgumentException("Director name cannot be empty.");
            if (releaseYear < 1000 || releaseYear > 9999)
                throw new ArgumentException("Release year must be a 4-digit number.");

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("         WELCOME TO YMDB\n");
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
                    Console.Write("Exiting program"); // no newline
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
                    1) Add a movie you've watched/you want to watch it later
                    2) Delete a movie from watchlist
                    3) Update a movie(if you watched it)
                    4) Search movies by year
                    5) Search movies by title
                    6) Exit"
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

                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            }
        }

        static void Add()
        {
            System.Console.WriteLine("Add new movie to your watchlist or you've watched.");
            Console.Write("enter the title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            if (title.Length > 100)
            {
                Console.WriteLine("Title cannot exceed 100 characters.");
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

            Movie movie=new Movie(title,director,releaseYear.Length==4?int.Parse(releaseYear):throw new ArgumentException("Release year must be a 4-digit number."),status);
        }

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
            System.Console.WriteLine("enter the title of the movie or enter 1 to go to main menu .");
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
            System.Console.WriteLine("enter the title of the movie or enter 1 to go to main menu .");

        }
        static void SearchByYear()
        { 
            
         }
        static void SearchByTitle() { }

    }
}