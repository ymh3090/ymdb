using System.Text.Json;
// This class is responsible for storing and retrieving movies from a JSON file

namespace ymdb
{
    public class MovieRepository
    {
        // path to our local storage file
        private const string FilePath = "movies.json";

        // list to hold all movies in memory while the program is running
        private List<Movie> movies;

        // Constructor — loads movies from file when the repository is created
        public MovieRepository()
        {
            LoadFromFile();
        }

        // Get all movies from the list
        public List<Movie> GetAll() => movies;

        // Add a new movie to the list and save changes
        public void Add(Movie movie)
        {
            movies.Add(movie);
            SaveToFile();
        }

        // Delete a movie by title (returns true if deleted successfully)
        public bool Delete(string title)
        {
            var movie = movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                movies.Remove(movie);
                SaveToFile();
                return true;
            }
            return false;
        }

        // Update a movie's status to "Watched"
        public bool UpdateStatusToWatched(string title)
        {
            var movie = movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                movie.Status = MovieStatus.Watched;
                SaveToFile();
                return true;
            }
            return false;
        }

        // Search movies by title (case-insensitive)
        public List<Movie> SearchByTitle(string title) =>
            movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        // Search movies by exact year
        public List<Movie> SearchByYear(int year) =>
            movies.Where(m => m.ReleaseYear == year).ToList();

        // Sort movies alphabetically by title
        public List<Movie> SortByTitle() => movies.OrderBy(m => m.Title).ToList();

        // Sort movies by release year
        public List<Movie> SortByYear() => movies.OrderBy(m => m.ReleaseYear).ToList();

        // Save the movie list to the JSON file
        private void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // Makes JSON readable
            File.WriteAllText(FilePath, JsonSerializer.Serialize(movies, options));
        }

        // Load movies from the JSON file (or create a new list if file doesn't exist)
        private void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                movies = JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
            }
            else
            {
                movies = new List<Movie>();
            }
        }
    }
}
