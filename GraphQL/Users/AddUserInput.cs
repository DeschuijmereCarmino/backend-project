namespace backendProject.API.GraphQl.Users;
public record AddUserInput(string username, string password, string email, List<UserMovie> movies, List<Crew> crew);