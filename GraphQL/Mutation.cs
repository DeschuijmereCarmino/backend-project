namespace backendProject.GraphQl.Mutations;

public class Mutation
{
    public async Task<AddMoviePayload> AddMovie([Service] IMovieService movieService, AddMovieInput input)
    {
        var newMovie = new Movie()
        {
            Title = input.title,
            Description = input.description,
            ReleaseDate = input.releaseDate,
            Crew = input.crew
        };

        var created = await movieService.AddMovieAsync(newMovie);
        return new AddMoviePayload(created);
    }
}