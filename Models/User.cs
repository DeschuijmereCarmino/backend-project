namespace backendProject.API.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<Movie>? Movies { get; set; }
    public List<Crew>? Crew { get; set; }
}