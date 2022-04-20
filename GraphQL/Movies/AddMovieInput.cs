namespace backendProject.API.GraphQl.Movies;
public record AddMovieInput(string title, string description, string releaseDate, List<Crew> crew);