namespace backendProject.API.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ReleaseDate { get; set; }
    public List<Crew>? Crew { get; set; }
}