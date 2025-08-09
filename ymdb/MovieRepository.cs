using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ymdb
{
    public class MovieRepository
    {
        // File will be stored directly on the Desktop, regardless of username
        private static readonly string FilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Movies.json");

        private List<Movie> movies;

        public MovieRepository()
        {
            Load();
        }

        // Load movies from JSON file (create file if missing)
        private void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                movies = JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
            }
            else
            {
                movies = new List<Movie>();
                Save(); // Create the file immediately
            }
        }

        // Save movies to JSON file
        private void Save()
        {
            string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Add a movie
        public void Add(Movie movie)
        {
            movies.Add(movie);
            Save();
        }

        // Delete by title
        public bool DeleteMovie(string title)
        {
            var movie = movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                movies.Remove(movie);
                Save();
                return true;
            }
            return false;
        }

        // Update status to Watched
        public bool UpdateToWatched(string title)
        {
            var movie = movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                movie.Status = MovieStatus.Watched;
                Save();
                return true;
            }
            return false;
        }

        // Search by year
        public List<Movie> SearchByYear(int year)
        {
            return movies.Where(m => m.ReleaseYear == year).ToList();
        }

        // Search by title
        public List<Movie> SearchByTitle(string title)
        {
            return movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Sort by year
        public List<Movie> SortByYear()
        {
            return movies.OrderBy(m => m.ReleaseYear).ToList();
        }

        // Sort by title
        public List<Movie> SortByTitle()
        {
            return movies.OrderBy(m => m.Title).ToList();
        }

        // Get all movies
        public List<Movie> GetAllMovies()
        {
            return movies;
        }
    }
}
