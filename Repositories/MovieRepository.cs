namespace backendProject.API.Repositories;

public interface IMovieRepository
{

    Task<Movie> AddMovieAsync(Movie newMovie);
    Task<List<Movie>> AddMoviesAsync(List<Movie> newMovies);
    Task<Movie> GetMovieAsync(string id);
    Task<List<Movie>> GetMoviesAsync();
    Task<List<Movie>> GetMoviesByIdsAsync(List<string> ids);
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
        try
        {
            // var result = await _context.MoviesCollection.Find<Movie>(m => m.Title == newMovie.Title).FirstOrDefaultAsync();

            // if (result != null)
            // {
            //     return null!;
            // }

            await _context.MoviesCollection.InsertOneAsync(newMovie);
            return newMovie;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Movie>> AddMoviesAsync(List<Movie> newMovies)
    {
        await _context.MoviesCollection.InsertManyAsync(newMovies);
        return newMovies;
    }

    public async Task<Movie> GetMovieAsync(string id) => await _context.MoviesCollection.Find<Movie>(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<List<Movie>> GetMoviesAsync() => await _context.MoviesCollection.Find(_ => true).ToListAsync();

    public async Task<List<Movie>> GetMoviesByIdsAsync(List<string> ids)
    {
        try
        {
            List<Movie> movies = new List<Movie>();

            foreach (var id in ids)
            {
                var movie = await _context.MoviesCollection.Find<Movie>(m => m.Id == id).FirstOrDefaultAsync();
                movies.Add(movie);
            }

            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

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