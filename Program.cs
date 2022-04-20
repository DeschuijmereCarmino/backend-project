var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseConfig>(mongoSettings);

var MailSettings = builder.Configuration.GetSection("Mailling");
builder.Services.Configure<MailConfig>(MailSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICrewRepository, CrewRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CrewValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MovieValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", (IMovieService movieService) =>
{
    movieService.SetupData();
    return Results.Ok("DummyData added");
});

app.MapPost("/actorName", async (IMovieService movieService, string name) =>
{
    var id = await movieService.GetIdByCrewMemberNameAsync(name);
    return Results.Ok(id);
});

app.MapGet("/actors", async (IMovieService movieService) =>
{
    var actors = await movieService.GetActorCrewAsync();
    return Results.Ok(actors);
});

app.MapGet("/crew", async (IMovieService movieService) =>
{
    var crew = await movieService.GetCrewAsync();
    return Results.Ok(crew);
});

app.MapGet("/directors", async (IMovieService movieService) =>
{
    var directors = await movieService.GetDirectorCrewAsync();
    return Results.Ok(directors);
});

app.MapGet("emails", async (IMovieService movieService) =>
{
    var emails = await movieService.GetEmailAsync();
    return Results.Ok(emails);
});

app.MapPost("/movie", async (IMovieService movieService, IValidator<Movie> validator, Movie movie) =>
{
    var validationResult = validator.Validate(movie);
    if (validationResult.IsValid)
    {
        var result = await movieService.AddMovieAsync(movie);
        return Results.Created($"movie/{result.Id}", result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapGet("/movie/{id}", async (IMovieService movieService, string id) =>
{
    var result = await movieService.GetMovieAsync(id);
    return Results.Ok(result);
});

app.MapGet("/movies", async (IMovieService movieService) =>
{
    var movies = await movieService.GetMoviesAsync();
    return Results.Ok(movies);
});

app.MapGet("/sendmail", async (IMovieService movieService) =>
{
    await movieService.SendMailAsync();
});

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

// app.MapPost("/user", async (IMovieService movieService, IValidator<User> validator, User user) =>
// {
//     var validationResult = validator.Validate(user);
//     if (validationResult.IsValid)
//     {
//         var result = await movieService.AddUserAsync(user);
//         return Results.Created($"user/{result.Id}", result);
//     }
//     else
//     {
//         var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
//         return Results.BadRequest(errors);
//     }
// });

app.MapGet("/user/{id}", async (IMovieService movieService, string id) =>
{
    var result = await movieService.GetUserAsync(id);
    return Results.Ok(result);
});

app.MapGet("users", async (IMovieService movieService) =>
{
    var users = await movieService.GetUsersAsync();
    return Results.Ok(users);
});

app.MapPost("/authenticate", (IAuthenticationService authenticationService, IOptions<ApiKeyConfig> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if (user == null)
        return Results.Unauthorized();

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.APIKey!));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));
    claimsForToken.Add(new Claim("city", user.city));

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
// app.Run();
public partial class Program { }