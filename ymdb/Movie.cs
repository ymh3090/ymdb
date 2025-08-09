// this file was to make the data

namespace ymdb
{
    // Enum for movie status (either already watched or planned to watch)
    public enum MovieStatus
    {
        Watched,
        Watchlisted
    }

    public class Movie
    {
        // Properties of the movie
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public MovieStatus Status { get; set; }
        public string Genre { get; set; }

        // Constructor to intitate a movie object
        public Movie(string title, string director, int releaseYear, MovieStatus status, string genre = "Unknown") // Genre defaults to "Unknown"
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Status = status;
            Genre = genre;
        }

        // Overriding ToString() to display movie details nicely in the console
        public string print()
        {
            return $"Title: {Title}, Director: {Director}, Year: {ReleaseYear}, Genre: {Genre}\n Status: {Status}";
        }
    }
}