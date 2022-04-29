namespace backendProject.API.Repositories;

public interface IUserRepository
{
    Task<List<string>> GetEmailsAsync();
    Task<User> AddUserAsync(User user);
    Task<User> AddUserCrewAsync(string id, Crew crew);
    Task<User> AddUserMovieAsync(string id, UserMovie movie);
    Task<List<User>> AddUsersAsync(List<User> newUsers);
    Task<User> GetUserAsync(string id);
    Task<List<string>> GetUserMovieIdsAsync(string id);
    Task<List<User>> GetUsersAsync();
    Task<User> LoginUserAsync(string username, string password);
    Task<User> RemoveUserCrewAsync(string id, Crew crew);
    Task<User> RemoveUserMovieAsync(string id, string movieId);
    Task<User> UpdateUserCrewAsync(string id, Crew crew);
    // Task<User> UpdateUserEmailAsync(string id, string email);
    Task<User> UpdateUserLoginAsync(string id, UserLogin userLogin);
    // Task<User> UpdateUserPasswordAsync(string id, string password);
    Task<User> UpdateUserMoviesAsync(string id, Movie movie);
}

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;

    public UserRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<String>> GetEmailsAsync()
    {
        var emails = new List<String>();
        var users = await GetUsersAsync();

        foreach (var user in users)
        {
            emails.Add(user.Email!);
        }

        return emails;
    }

    public async Task<User> AddUserAsync(User newUser)
    {
        await _context.UsersCollection.InsertOneAsync(newUser);
        return newUser;
    }

    public async Task<User> AddUserCrewAsync(string userId, Crew crew)
    {
        try
        {
            var user = await _context.UsersCollection.Find<User>(u => u.Id == userId).FirstOrDefaultAsync();
            user.Crew!.Add(crew);
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<User> AddUserMovieAsync(string userId, UserMovie movie)
    {
        try
        {
            var user = await _context.UsersCollection.Find<User>(u => u.Id == userId).FirstOrDefaultAsync();
            user.Movies!.Add(movie);
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<User>> AddUsersAsync(List<User> newUsers)
    {
        await _context.UsersCollection.InsertManyAsync(newUsers);
        return newUsers;
    }

    public async Task<User> GetUserAsync(string id) => await _context.UsersCollection.Find<User>(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<List<string>> GetUserMovieIdsAsync(string id)
    {
        try
        {
            List<string> ids = new List<string>();

            var user = await _context.UsersCollection.Find<User>(u => u.Id == id).FirstOrDefaultAsync();

            foreach (var movie in user.Movies!)
            {
                ids.Add(movie.Id!);
            }

            return ids;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<User>> GetUsersAsync() => await _context.UsersCollection.Find(_ => true).ToListAsync();

    public async Task<User> LoginUserAsync(string username, string password)
    {
        try
        {
            var user = await _context.UsersCollection.Find<User>(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }

            return null!;

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<User> RemoveUserCrewAsync(string id, Crew crew)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var update = Builders<User>.Update.Pull("Crew", crew);
            var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
            return await GetUserAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<User> RemoveUserMovieAsync(string id, string movieId)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var update = Builders<User>.Update.PullFilter(u => u.Movies,
                m => m.Id == movieId);

            var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
            return await GetUserAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<User> UpdateUserLoginAsync(string id, UserLogin userLogin)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var update = Builders<User>.Update.Set("Email", userLogin.Email).Set("Password", userLogin.Password);
            var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
            return await GetUserAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    // public async Task<User> UpdateUserEmailAsync(string id, string email)
    // {
    //     try
    //     {
    //         var filter = Builders<User>.Filter.Eq("Id", id);
    //         var update = Builders<User>.Update.Push("Email", email);
    //         var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
    //         return await GetUserAsync(id);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex);
    //         throw;
    //     }
    // }

    // public async Task<User> UpdateUserPasswordAsync(string id, string password)
    // {
    //     try
    //     {
    //         var filter = Builders<User>.Filter.Eq("Id", id);
    //         var update = Builders<User>.Update.Push("Password", password);
    //         var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
    //         return await GetUserAsync(id);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex);
    //         throw;
    //     }
    // }

    public async Task<User> UpdateUserCrewAsync(string id, Crew crew)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var update = Builders<User>.Update.Push("Crew", crew);
            var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
            return await GetUserAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<User> UpdateUserMoviesAsync(string id, Movie movie)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var update = Builders<User>.Update.Push("Movies", movie);
            var result = await _context.UsersCollection.UpdateOneAsync(filter, update);
            return await GetUserAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

}