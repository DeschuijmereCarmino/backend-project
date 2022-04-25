public record UserInfo(string id, string name);
public record AuthenticationRequestBody(string username, string password);

public interface IAuthenticationService
{
    Task<UserInfo> ValidateUser(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserInfo> ValidateUser(string username, string password)
    {

        var user = await _userRepository.LoginUserAsync(username, password);
        
        if (user == null)
        {
            return null!;
        }
        else
        {
            return new UserInfo(user.Id!, user.Username!);
        }

    }
}