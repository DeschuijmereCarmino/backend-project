namespace backendProject.API.Repositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<List<User>> AddUsersAsync(List<User> newUsers);
    Task<User> GetUserAsync(string id);
    Task<List<User>> GetUsersAsync();
    Task<List<string>> GetEmailsAsync();
}

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;

    public UserRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User newUser)
    {
        await _context.UsersCollection.InsertOneAsync(newUser);
        return newUser;
    }

    public async Task<List<User>> AddUsersAsync(List<User> newUsers)
    {
        await _context.UsersCollection.InsertManyAsync(newUsers);
        return newUsers;
    }

    public async Task<User> GetUserAsync(string id) => await _context.UsersCollection.Find<User>(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<List<User>> GetUsersAsync() => await _context.UsersCollection.Find(_ => true).ToListAsync();

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

}