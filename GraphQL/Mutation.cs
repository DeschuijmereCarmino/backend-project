namespace backendProject.API.GraphQl.Mutations;

public class Mutation
{
    public async Task<AddMoviePayload> AddMovie([Service] IMovieService movieService, AddMovieInput input, [Service]IValidator<Movie> validator)
    {
        var newMovie = new Movie()
        {
            Title = input.title,
            Description = input.description,
            ReleaseDate = input.releaseDate,
            Crew = input.crew
        };

        var validationResult = validator.Validate(newMovie);
        if (validationResult.IsValid)
        {
            var created = await movieService.AddMovieAsync(newMovie);
            return new AddMoviePayload(created);
        }
        
        string message = string.Empty;
        foreach (var error in validationResult.Errors)
        {
            message += $" {error.ErrorMessage},";
        }
        throw new Exception(message);
    }

    public async Task<AddCrewPayload> AddCrew([Service] IMovieService movieService, AddCrewInput input, [Service]IValidator<Crew> validator)
    {
        var newCrew = new Crew()
        {
            Name = input.name,
            Type = input.type
        };

        var validationResult = validator.Validate(newCrew);
        if (validationResult.IsValid)
        {

            var created = await movieService.AddCrewMemberAsync(newCrew);
            return new AddCrewPayload(created);
        }

        string message = string.Empty;
        foreach (var error in validationResult.Errors)
        {
            message += $" {error.ErrorMessage},";
        }
        throw new Exception(message);

    }

    public async Task<AddUserPayload> AddUser([Service] IMovieService movieService, AddUserInput input, [Service] IValidator<User> validator)
    {
        var newUser = new User()
        {
            Username = input.username,
            Password = input.password,
            Email = input.email,
            Crew = input.crew,
            Movies = input.movies
        };
        
        var validationResult = validator.Validate(newUser);
        if (validationResult.IsValid)
        {
            var created = await movieService.AddUserAsync(newUser);
            return new AddUserPayload(created);
        }

        string message = string.Empty;
        foreach (var error in validationResult.Errors)
        {
            message += $" {error.ErrorMessage},";
        }
        throw new Exception(message);

    }
}