using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMDB_CLI_Tool.Models;

namespace TMDB_CLI_Tool.Helpers
{
    public static class TmdbHelper
    {
        private static readonly string BearerToken = Environment.GetEnvironmentVariable("TMDB_AUTH_TOKEN");

        

        public static async Task<List<Genre>> GetGenresAsync()
        {
            var genreOptions = new RestClientOptions("https://api.themoviedb.org/3/genre/movie/list?language=es-MX");
            var genreClient = new RestClient(genreOptions);
            var genreRequest = new RestRequest("");
            genreRequest.AddHeader("accept", "application/json");
            genreRequest.AddHeader("Authorization", $"Bearer {BearerToken}");
            var genreResponse = await genreClient.GetAsync(genreRequest);
            var genreResponseObj = JsonConvert.DeserializeObject<GenreResponse>(genreResponse.Content);
            return genreResponseObj?.Genres ?? new List<Genre>();
        }


        public static async Task<List<Movie>> GetMoviesAsync(string endpoint, bool getAllPages = false, int totalPages = 1)
        {
            // Obtener la lista de géneros solo una vez
            var genreList = await GetGenresAsync();
            List<Movie> allMovies = new List<Movie>();

            try
            {
                // Si no queremos todas las páginas, podemos especificar cuántas páginas queremos.
                if (!getAllPages)
                {
                    var movieOptions = new RestClientOptions($"{endpoint}?language=es-ES&page=1&region=AR");
                    var movieClient = new RestClient(movieOptions);
                    var movieRequest = new RestRequest("");
                    movieRequest.AddHeader("accept", "application/json");
                    movieRequest.AddHeader("Authorization", $"Bearer {BearerToken}");
                    var movieResponse = await movieClient.GetAsync(movieRequest);

                    // Si la respuesta es exitosa, deserializamos el contenido
                    var movieResponseObj = JsonConvert.DeserializeObject<MovieResponse>(movieResponse.Content);
                    allMovies.AddRange(movieResponseObj.Result);
                }
                else
                {
                    for (int page = 1; page <= totalPages; page++)
                    {
                        var movieOptions = new RestClientOptions($"{endpoint}?language=es-ES&page={page}&region=AR");
                        var movieClient = new RestClient(movieOptions);
                        var movieRequest = new RestRequest("");
                        movieRequest.AddHeader("accept", "application/json");
                        movieRequest.AddHeader("Authorization", $"Bearer {BearerToken}");
                        var movieResponse = await movieClient.GetAsync(movieRequest);

                        var movieResponseObj = JsonConvert.DeserializeObject<MovieResponse>(movieResponse.Content);
                        totalPages = movieResponseObj.total_pages;

                        allMovies.AddRange(movieResponseObj.Result);
                    }
                }

                // Agregar nombres de géneros a las películas
                foreach (var movie in allMovies)
                {
                    movie.GenreNames = movie.genre_ids
                        .Select(id => genreList.FirstOrDefault(g => g.Id == id)?.Name)
                        .Where(name => name != null)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error en la solicitud
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return allMovies;
        }

        public static async Task<List<Movie>> GetNowPlayingMoviesAsync()
        {
            
            return await GetMoviesAsync("https://api.themoviedb.org/3/movie/now_playing", getAllPages: true);
        }

        public static async Task<List<Movie>> GetPopularMoviesAsync()
        {
            
            return await GetMoviesAsync("https://api.themoviedb.org/3/movie/popular");
        }

        public static async Task<List<Movie>> GetTopRatedMoviesAsync()
        {
            return await GetMoviesAsync("https://api.themoviedb.org/3/movie/top_rated?language=es-MX&page=1&region=AR");
        }

        public static async Task<List<Movie>> GetUpcomingMoviesAsync()
        {
            return await GetMoviesAsync("https://api.themoviedb.org/3/movie/upcoming?language=es-MX&page=1&region=AR");
        }
        public static async Task<Movie> GetMovieDetailsAsync(int movieId)
        {
            var movieOptions = new RestClientOptions($"https://api.themoviedb.org/3/movie/{movieId}?language=es-ES");
            var movieClient = new RestClient(movieOptions);
            var movieRequest = new RestRequest("");
            movieRequest.AddHeader("accept", "application/json");
            movieRequest.AddHeader("Authorization", $"Bearer {BearerToken}");

            try
            {
                var movieResponse = await movieClient.GetAsync(movieRequest);
                if (movieResponse.IsSuccessful)
                {
                    // Deserializamos la respuesta a un objeto MovieDetail
                    var movieDetail = JsonConvert.DeserializeObject<Movie>(movieResponse.Content);
                    return movieDetail;
                }
                else
                {
                    Console.WriteLine("Error fetching movie details: " + movieResponse.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public static void ShowMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"Title: {movie.Title}");
                Console.WriteLine($"Release Date: {movie.release_date}");
                Console.WriteLine($"Overview: {movie.Overview}");
                Console.WriteLine($"Vote Average: {movie.vote_average}");
                Console.WriteLine("Genres: " + string.Join(", ", movie.GenreNames));
                Console.WriteLine($"Poster: https://image.tmdb.org/t/p/w500{movie.poster_path}");
                Console.WriteLine("------------------------------------");
            }
        }
    }
}
