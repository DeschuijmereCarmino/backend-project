namespace backendProject.API.Repositories;

public class UserRepository
{

    private readonly IMongoContext _context;

    public UserRepository(IMongoContext context)
    {
        _context = context;
    }

    //return list of emails of all users
    public async Task<List<String>> GetEmailsByIds(List<string> ids)
    {
        var emails = new List<String>();

        foreach (var id in ids)
        {
            var user = await _context.UsersCollection.Find<User>(u => u.Id == id).FirstOrDefaultAsync();
            emails.Add(user.Email!);
        }

        return emails;
    }
}