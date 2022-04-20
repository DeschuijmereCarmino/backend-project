namespace backendProject.API.Models;

public class Crew
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
}