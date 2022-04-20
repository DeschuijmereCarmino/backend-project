namespace backendProject.API.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(m => m.Title).NotEmpty().WithMessage("Geef een titel op");
        RuleFor(m => m.Description).NotEmpty().WithMessage("Geef een omschrijving op");
        RuleFor(m => m.ReleaseDate).NotEmpty().WithMessage("Geef een release date op");
        RuleFor(m => m.Crew).NotEmpty().WithMessage("Geef minstens 1 crewlid op");
    }
}