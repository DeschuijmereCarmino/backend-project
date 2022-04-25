namespace backendProject.API.Repositories;

public interface ICrewRepository
{
    Task<List<Crew>> AddCrewAsync(List<Crew> newCrew);
    Task<Crew> AddCrewMemberAsync(Crew newCrew);
    Task<List<Crew>> GetActorCrewAsync();
    Task<List<Crew>> GetCrewAsync();
    Task<List<Crew>> GetDirectorCrewAsync();
    Task<Crew> GetCrewMemberAsync(string id);
    Task<string> GetIdByCrewMemberNameAsync(string name);

}

public class CrewRepository : ICrewRepository
{
    private readonly IMongoContext _context;

    public CrewRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Crew> AddCrewMemberAsync(Crew newCrew)
    {
        try
        {
            // var result = await _context.CrewCollection.Find<Crew>(c => c.Name == newCrew.Name).FirstOrDefaultAsync();

            // if (result != null)
            // {
            //     return null!;
            // }

            await _context.CrewCollection.InsertOneAsync(newCrew);
            return newCrew;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
 
    }

    public async Task<List<Crew>> AddCrewAsync(List<Crew> newCrew)
    {
        try
        {
            // var result = await _context.CrewCollection.Find<Crew>(c => c.Name == newCrew.Name).FirstOrDefaultAsync();

            // if (result != null)
            // {
            //     return null!;
            // }    

            await _context.CrewCollection.InsertManyAsync(newCrew);
            return newCrew;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Crew> GetCrewMemberAsync(string id) => await _context.CrewCollection.Find<Crew>(a => a.Id == id).FirstOrDefaultAsync();

    public async Task<List<Crew>> GetActorCrewAsync()
    {
        List<Crew> actors = new List<Crew>();

        var allCrew = await GetCrewAsync();

        foreach (var crew in allCrew)
        {
            if (crew.Type == "Actor")
            {
                actors.Add(crew);
            }
        }

        return actors;
    }

    public async Task<List<Crew>> GetCrewAsync() => await _context.CrewCollection.Find(_ => true).ToListAsync();

    public async Task<List<Crew>> GetDirectorCrewAsync()
    {
        List<Crew> directors = new List<Crew>();

        var allCrew = await GetCrewAsync();

        foreach (var crew in allCrew)
        {
            if (crew.Type == "Director")
            {
                directors.Add(crew);
            }
        }

        return directors;
    }

    public async Task<string> GetIdByCrewMemberNameAsync(string name)
    {
        try
        {
            var crew = await _context.CrewCollection.Find<Crew>(a => a.Name == name).FirstOrDefaultAsync();
            return crew.Id!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null!;
        }
    }

    // public async Task<Crew> UpdateCrewMemberAsync(string id, Crew crew)
    // {
    //     try
    //     {
    //         var filter = Builders<Crew>.Filter.Eq("Id", id);
    //         var update = Builders<Crew>.Update.Push("Movies", movie.Id);
    //         var result = await _context.CrewCollection.UpdateOneAsync(filter, update);
    //         return await GetActorAsync(id);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex);
    //         throw;
    //     }
    // }
}