namespace backendProject.API.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Movie> MoviesCollection { get; }
    // IMongoCollection<Director> DirectorsCollection { get; }
    IMongoCollection<Crew> CrewCollection { get; }
    IMongoCollection<User> UsersCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Movie> MoviesCollection
    {
        get
        {
            return _database.GetCollection<Movie>(_settings.MoviesCollection);
        }
    }

    // public IMongoCollection<Director> DirectorsCollection
    // {
    //     get
    //     {
    //         return _database.GetCollection<Director>(_settings.DirectorsCollection);
    //     }
    // }

    public IMongoCollection<Crew> CrewCollection
    {
        get
        {
            return _database.GetCollection<Crew>(_settings.CrewCollection);
        }
    }

    public IMongoCollection<User> UsersCollection
    {
        get
        {
            return _database.GetCollection<User>(_settings.UsersCollection);
        }
    }
}