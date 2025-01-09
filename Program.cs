using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using TMDB_CLI_Tool.Helpers;
using TMDB_CLI_Tool.Models;

var rootCommand = new RootCommand
{
    new Option<string>(
    "--type",
    description: "Specifies the type of movie to display (playing, popular, top, upcoming).")
};

// Register the handler for the '--type' option
rootCommand.Handler = CommandHandler.Create(async (string type) =>
{
    if (string.IsNullOrEmpty(type))
    {
        Console.WriteLine("Please specify a movie type with --type.");
        return;
    }

    Console.WriteLine("------------------------------------");

    switch (type.ToLower())
    {
        
        case "playing":
            var nowPlayingMovies = await TmdbHelper.GetNowPlayingMoviesAsync();
            TmdbHelper.ShowMovies(nowPlayingMovies);
            break;
            
        case "popular":
            var popularMovies = await TmdbHelper.GetPopularMoviesAsync();
            TmdbHelper.ShowMovies(popularMovies);
            break;

        case "top":
            var topRatedMovies = await TmdbHelper.GetTopRatedMoviesAsync();
            TmdbHelper.ShowMovies(topRatedMovies);
            break;

        case "upcoming":
            var upcomingMovies = await TmdbHelper.GetUpcomingMoviesAsync();
            TmdbHelper.ShowMovies(upcomingMovies);
            break;

        default:
            Console.WriteLine("Type not valid. Use 'playing', 'popular', 'top', or 'upcoming'.");
            break;
    }
});

// Ejecutar el comando
await rootCommand.InvokeAsync(args);
