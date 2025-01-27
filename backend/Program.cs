// Método principal
using System.CommandLine.NamingConventionBinder;
using System.CommandLine;
using TMDB_CLI_Tool.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

var rootCommand = new RootCommand
{
    new Option<string>(
        "--type",
        description: "Specifies the type of movie to display (playing, popular, top, upcoming).")
};

// Handler para la opción '--type'
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

// Determinar si ejecutar como CLI o como servidor API
if (args.Contains("--api"))
{
    await RunApiServerAsync();
}
else
{
    await rootCommand.InvokeAsync(args);
}


async Task RunApiServerAsync()
{
    var builder = WebApplication.CreateBuilder();
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("angularApp",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
    });
    var app = builder.Build();

    // Definición de los endpoints
    app.MapGet("/movies/now-playing", async () =>
    {
        var nowPlayingMovies = await TmdbHelper.GetNowPlayingMoviesAsync();
        return Results.Ok(nowPlayingMovies);
    });

    app.MapGet("/movies/popular", async () =>
    {
        var popularMovies = await TmdbHelper.GetPopularMoviesAsync();
        return Results.Ok(popularMovies);
    });

    app.MapGet("/movies/top-rated", async () =>
    {
        var topRatedMovies = await TmdbHelper.GetTopRatedMoviesAsync();
        return Results.Ok(topRatedMovies);
    });

    app.MapGet("/movies/upcoming", async () =>
    {
        var upcomingMovies = await TmdbHelper.GetUpcomingMoviesAsync();
        return Results.Ok(upcomingMovies);
    });
    app.MapGet("/movies/{id}", async (int id) =>
    {
        var movie = await TmdbHelper.GetMovieDetailsAsync(id);
        if (movie == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(movie);
    });
    // Ruta por defecto para manejar endpoints no encontrados
    app.MapGet("/{*url}", () => Results.NotFound("Endpoint not found"));

    // Ejecuta el servidor en localhost:5000
    Console.WriteLine("API Server running on http://localhost:5000");

    app.UseCors("angularApp");
    await app.RunAsync();
}