# TMDB CLI Tool

This is a Command Line Interface (CLI) application that allows you to interact with the TMDB (The Movie Database) API to retrieve movie information.

## Features

The application allows you to display different types of movie data from TMDB by using the following commands:

1. **Playing Movies**: Display movies currently playing in theaters.
2. **Popular Movies**: Display the most popular movies.
3. **Top Movies**: Display the top-rated movies.
4. **Upcoming Movies**: Display movies that are coming soon.

### Available Commands

- `tmdb-app --type "playing"`: Shows movies currently playing.
- `tmdb-app --type "popular"`: Shows popular movies.
- `tmdb-app --type "top"`: Shows top-rated movies.
- `tmdb-app --type "upcoming"`: Shows upcoming movies.

## Requirements

To run the CLI application, you will need the following:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or higher)
- A valid **TMDB API Key**. You can obtain your key by creating an account on [TMDb](https://www.themoviedb.org/) and following their API documentation.

## Setup

1. **Clone the Repository:**
   Begin by cloning the repository to your local machine:
   
   ```bash
   git clone https://github.com/JoaquinKorol/TMBD-CLI-Tool.git
   cd TMBD-CLI-Tool
   ## Configure the TMDB API Key:

1. Open the `appsettings.json` file located in the root directory of the project.
2. Replace the `API_KEY` placeholder with your valid TMDB API Key. You can obtain your key from [TMDb's API page](https://www.themoviedb.org/settings/api).

Example:

```json
{
  "TMDB_API_KEY": "your_api_key_here"
}
```

## Install Dependencies:

Run the following command to restore the project dependencies:

```bash
dotnet restore
```

## Run the Application:

After successfully setting up, you can run the application with the desired type of movie data:

```bash
dotnet run --type "popular"
```

You can replace `"popular"` with one of the following types to display different movie data:

- `"playing"`: Movies currently playing in theaters.
- `"popular"`: The most popular movies.
- `"top"`: Top-rated movies.
- `"upcoming"`: Movies coming soon.

### Example Commands

- **Display movies currently playing:**

```bash
dotnet run --type "playing"
```

- **Display the most popular movies:**

```bash
dotnet run --type "popular"
```

- **Display top-rated movies:**

```bash
dotnet run --type "top"
```

- **Display upcoming movies:**

```bash
dotnet run --type "upcoming"
```

## Project

Project taken from [https://roadmap.sh/projects/tmdb-cli](https://roadmap.sh/projects/tmdb-cli)
