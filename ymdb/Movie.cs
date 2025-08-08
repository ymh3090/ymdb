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
        public string Genre { get; set; }

        public Movie(string title, string director, int releaseYear, MovieStatus status, string genre = "Unknown")
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Status = status;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Director: {Director}, Year: {ReleaseYear}, Genre: {Genre}, Status: {Status}";
        }
    }
}
