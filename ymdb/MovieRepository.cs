using System.Text.Json;

namespace ymdb
{
    public class MovieRepository
    {
        private const string FilePath = "movies.json";
        private List<Movie> movies;

        public MovieRepository()
        {
            LoadFromFile();
        }

        public List<Movie> GetAll() => movies;

        public void Add(Movie movie)
        {
            movies.Add(movie);
            SaveToFile();
        }

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

        public List<Movie> SearchByTitle(string title) =>
            movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Movie> SearchByYear(int year) =>
            movies.Where(m => m.ReleaseYear == year).ToList();

        public List<Movie> SortByTitle() => movies.OrderBy(m => m.Title).ToList();

        public List<Movie> SortByYear() => movies.OrderBy(m => m.ReleaseYear).ToList();

        private void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(movies, options));
        }

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
