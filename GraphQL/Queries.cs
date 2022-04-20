namespace backendProject.API.GraphQl.Queries;

public class Queries
{
    public async Task<List<Movie>> GetMoviesAsync([Service] IMovieService movieService) => await movieService.GetMoviesAsync();

    public async Task<List<Crew>> GetCrewAsync([Service] IMovieService movieService) => await movieService.GetCrewAsync();
}