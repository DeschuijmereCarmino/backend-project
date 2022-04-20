namespace backendProject.API.Repositories;

public interface IMovieRepository
{

    Task<Movie> AddMovieAsync(Movie newMovie);
    Task<List<Movie>> AddMoviesAsync(List<Movie> newMovies);
    Task<Movie> GetMovieAsync(string id);
    Task<List<Movie>> GetMoviesAsync();
    Task<Movie> UpdateMovieCrewMemberAsync(string movieId, Crew crew);
}

public class MovieRepository : IMovieRepository
{
    private readonly IMongoContext _context;

    public MovieRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddMovieAsync(Movie newMovie)
    {
        await _context.MoviesCollection.InsertOneAsync(newMovie);
        return newMovie;
    }

    public async Task<List<Movie>> AddMoviesAsync(List<Movie> newMovies)
    {
        await _context.MoviesCollection.InsertManyAsync(newMovies);
        return newMovies;
    }

    public async Task<Movie> GetMovieAsync(string id) => await _context.MoviesCollection.Find<Movie>(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<List<Movie>> GetMoviesAsync() => await _context.MoviesCollection.Find(_ => true).ToListAsync();

    public async Task<Movie> UpdateMovieCrewMemberAsync(string movieId, Crew crew)
    {
        try
        {
            var filter = Builders<Movie>.Filter.Eq("Id", movieId);
            var update = Builders<Movie>.Update.Push("Crew", crew);
            var result = await _context.MoviesCollection.UpdateOneAsync(filter, update);
            return await GetMovieAsync(movieId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}