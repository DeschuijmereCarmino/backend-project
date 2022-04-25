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

    public async Task<AddCrewPayload> AddCrew([Service] IMovieService movieService, AddCrewInput input)
    {
        var newCrew = new Crew()
        {
            Name = input.name,
            Type = input.type
        };

        var created = await movieService.AddCrewMemberAsync(newCrew);
        return new AddCrewPayload(created);
    }

    public async Task<AddUserPayload> AddUser([Service] IMovieService movieService, AddUserInput input)
    {
        var newUser = new User()
        {
            Username = input.username,
            Password = input.password,
            Email = input.email,
            Crew = input.crew,
            Movies = input.movies
        };

        var created = await movieService.AddUserAsync(newUser);
        return new AddUserPayload(created);
    }
}