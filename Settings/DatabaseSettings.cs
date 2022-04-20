namespace backendProject.API.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? CrewCollection { get; set; }
    public string? MoviesCollection { get; set; }
    public string? UsersCollection { get; set; }
}