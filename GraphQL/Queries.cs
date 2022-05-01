namespace backendProject.API.GraphQl.Queries;

public class Queries
{
    [UseFiltering]
    [UseSorting]
    public async Task<Crew> GetCrewMemberAsync([Service] IMovieService movieService, string id) => await movieService.GetCrewMemberAsync(id);
    public async Task<List<Crew>> GetCrewAsync([Service] IMovieService movieService) => await movieService.GetCrewAsync();
    public async Task<Movie> GetMovieAsync([Service] IMovieService movieService, string id) => await movieService.GetMovieAsync(id);
    public async Task<List<Movie>> GetMoviesAsync([Service] IMovieService movieService) => await movieService.GetMoviesAsync();
    public async Task<User> GetUserAsync([Service] IMovieService movieService, string id) => await movieService.GetUserAsync(id);
    public async Task<List<User>> GetUsersAsync([Service] IMovieService movieService) => await movieService.GetUsersAsync();

}