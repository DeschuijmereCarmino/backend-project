var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseConfig>(mongoSettings);

var MailSettings = builder.Configuration.GetSection("Mailing");
builder.Services.Configure<MailConfig>(MailSettings);

var apiKeySettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<ApiKeyConfig>(apiKeySettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICrewRepository, CrewRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IMailService, MailService>();

//commentaar zetten voor testen
builder.Services.AddHostedService<Worker>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CrewValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MovieValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserMovieValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .AddFiltering()
    .AddSorting()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:APIKey"]))
        };
    }
);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.UseSwaggerUI();

app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", (IMovieService movieService) =>
{
    movieService.SetupData();
    return Results.Ok("DummyData added");
});

//Get Crew Id from name
app.MapPost("/crewName", async (IMovieService movieService, IValidator<Crew> validator, Crew crew) =>
{
    var validationResult = validator.Validate(crew);
    if (validationResult.IsValid)
    {
        var id = await movieService.GetIdByCrewMemberNameAsync(crew.Name!);
        if (id == null)
            return Results.NotFound();

        return Results.Ok(id);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }

});

//Add Crew
app.MapPost("/crew", async (IMovieService movieService, IValidator<Crew> validator, Crew crew) =>
{
    var validationResult = validator.Validate(crew);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddCrewMemberAsync(crew);

        // if (result == null)
        //     return Results.Conflict();

        return Results.Created($"crew/{result.Id}", result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

//Get Crew Actors
app.MapGet("/actors", async (IMovieService movieService) =>
{
    var actors = await movieService.GetActorCrewAsync();
    return Results.Ok(actors);
});

//Get CrewMember
app.MapGet("/crew/{crewId}", async (IMovieService movieService, string crewId) =>
{
    var result = await movieService.GetCrewMemberAsync(crewId);

    if (result == null)
        return Results.NotFound();

    return Results.Ok(result);
});

//Get Crew
app.MapGet("/crew", async (IMovieService movieService) =>
{
    var crew = await movieService.GetCrewAsync();
    return Results.Ok(crew);
});

//Get Crew Directors
app.MapGet("/directors", async (IMovieService movieService) =>
{
    var directors = await movieService.GetDirectorCrewAsync();
    return Results.Ok(directors);
});

//Get Users emails
app.MapGet("/emails", async (IMovieService movieService) =>
{
    var emails = await movieService.GetEmailAsync();
    return Results.Ok(emails);
});

//Add Movie
app.MapPost("/movie", async (IMovieService movieService, IValidator<Movie> validator, Movie movie) =>
{
    var validationResult = validator.Validate(movie);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddMovieAsync(movie);

        // if (result == null)
        //     return Results.Conflict();

        return Results.Created($"movie/{result.Id}", result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

//Add Movies =>DTO nodig voor json naar list conversie
// app.MapPost("/movies", async (IMovieService movieService, IValidator<Movie> validator, List<Movie> movies) =>
// {
//     foreach (var movie in movies)
//     {
//         var validationResult = validator.Validate(movie);
//         if (!validationResult.IsValid)
//         {
//             var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
//             return Results.BadRequest(errors);
//         }
//     }

//     var result = await movieService.AddMoviesAsync(movies);
//     // return Results.Ok("movies created");
//     return Results.Created("/movies", result);
// });

//Get Movie
app.MapGet("/movie/{id}", async (IMovieService movieService, string id) =>
{
    var result = await movieService.GetMovieAsync(id);


    if (result == null)
        return Results.NotFound();

    return Results.Ok(result);
});

//Get Movies
app.MapGet("/movies", async (IMovieService movieService) =>
{
    var movies = await movieService.GetMoviesAsync();
    return Results.Ok(movies);
});

//SendEmail 
app.MapGet("/sendmail", async (IMailService mailService, IMovieService movieService) =>
{
    List<string>? Emails = new List<string>();
    Emails.Add("carmino.deschuijmere@outlook.com");

    var movie = await movieService.GetMovieAsync("625ecf987726d02af8756043");
    await mailService.SendMailAsync(Emails, movie);
});

//Add User
app.MapPost("/user", async (IMovieService movieService, IValidator<User> validator, User user) =>
{
    var validationResult = validator.Validate(user);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddUserAsync(user);
        return Results.Created($"user/{result.Id}", result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

//Add User movie
app.MapPost("/user/{userId}/movie", async (IMovieService movieService, IValidator<UserMovie> validator, string userId, UserMovie movie) =>
{
    var validationResult = validator.Validate(movie);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddUserMovieAsync(userId, movie);
        return Results.Ok(result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

//Add User crew
app.MapPost("/user/{userId}/crew", async (IMovieService movieService, IValidator<Crew> validator, string userId, Crew crew) =>
{
    var validationResult = validator.Validate(crew);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddUserCrewAsync(userId, crew);
        return Results.Ok(result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

// //Add Users
// app.MapPost("/users",async (IMovieService movieService, IValidator<User> validator, List<User> users) =>
// {
//     foreach (var user in users)
//     {
//         var validationResult = validator.Validate(user);
//         if (!validationResult.IsValid)
//         {
//             var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
//             return Results.BadRequest(errors);
//         }
//     }

//     await movieService.AddUsersAsync(users);
//     return Results.Ok("users created");
// });

//Update User login
app.MapPost("/user/{id}/login", async (IMovieService movieService, IValidator<UserLogin> validator, string id, UserLogin userLogin) =>
{
    var validationResult = validator.Validate(userLogin);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
    
    var user = movieService.GetUserAsync(id);

    if (user == null)
    {
        return Results.NotFound();
    }

    var result = await movieService.UpdateUserLoginAsync(id, userLogin);
    return Results.Ok(result);
});

//Get User
app.MapGet("/user/{id}", async (IMovieService movieService, string id) =>
{
    var result = await movieService.GetUserAsync(id);
    return Results.Ok(result);
});

//Get User movies
app.MapGet("/user/{id}/movies", async (IMovieService movieService, string userId) =>
{
    var ids = await movieService.GetUserMovieIdsAsync(userId);

    if (ids == null)
        return Results.NotFound();

    var movies = await movieService.GetMoviesByIdsAsync(ids);

    return Results.Ok(movies);
});

//Get Users
app.MapGet("/users", [Authorize] async (IMovieService movieService, ClaimsPrincipal user) =>
{
    var users = await movieService.GetUsersAsync();
    return Results.Ok(users);
});

//Delete User Crew
app.MapDelete("/user/{userId}/crew/{crewId}", async (IMovieService movieService, string userId, string crewId) =>
{
    var user = await movieService.GetUserAsync(userId);
    var crew = await movieService.GetCrewMemberAsync(crewId);

    if (user == null)
    {
        return Results.NotFound();
    }

    if (crew == null)
    {
        return Results.NotFound();
    }

    var result = await movieService.RemoveUserCrewAsync(userId, crew);
    return Results.Ok(result);
});

//Delete User Movie
app.MapDelete("/user/{userId}/movie/{movieId}", async (IMovieService movieService, string userId, string movieId) =>
{
    var user = await movieService.GetUserAsync(userId);
    var movie = await movieService.GetMovieAsync(movieId);

    if (user == null)
    {
        return Results.NotFound();
    }

    if (movie == null)
    {
        return Results.NotFound();
    }

    var result = await movieService.RemoveUserMovieAsync(userId, movieId);
    return Results.Ok(result);
});

//Authenticate User for token
app.MapPost("/authenticate", async (IAuthenticationService authenticationService, IOptions<ApiKeyConfig> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = await authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);

    if (user == null)
        return Results.Unauthorized();

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.APIKey!));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", user.id));
    claimsForToken.Add(new Claim("given_name", user.name));

    var jwtSecurityToken = new JwtSecurityToken(
         authSettings.Value.Issuer,
         authSettings.Value.Audience,
         claimsForToken,
         DateTime.UtcNow,
         DateTime.UtcNow.AddHours(1),
         signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});

app.Run("http://0.0.0.0:3000");

//VOOR TESTEN
//app.Run();
public partial class Program { }