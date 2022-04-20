var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

var MailSettings = builder.Configuration.GetSection("Mailling");
builder.Services.Configure<MailConfig>(MailSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICrewRepository, CrewRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CrewValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MovieValidator>());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();

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

app.MapGet("/movies", async (IMovieService movieService) =>
{
    var movies = await movieService.GetMoviesAsync();
    return Results.Ok(movies);
});

app.MapGet("/test", async (IMovieService movieService) =>
{
    await movieService.SendMailAsync();
});
app.Run("http://0.0.0.0:3000");
