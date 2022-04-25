namespace backendProject.API.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    List<string> conditions = new List<string>() { "Actor", "Director" };
    public MovieValidator()
    {
        RuleFor(m => m.Title).NotNull().WithMessage("Geef een titel op").NotEmpty().WithMessage("Geef een titel op");
        RuleFor(m => m.Description).NotNull().WithMessage("Geef een omschrijving op").NotEmpty().WithMessage("Geef een omschrijving op");
        RuleFor(m => m.ReleaseDate).NotNull().WithMessage("Geef een release date op").NotEmpty().WithMessage("Geef een release date op");

        RuleForEach(m => m.Crew).NotEmpty().WithMessage("Geef minstens 1 crewlid op");
        RuleForEach(m => m.Crew).ChildRules(crew =>
        {
            crew.RuleFor(c => c.Id).NotEmpty().WithMessage("Crewlid ontbreekt id").NotNull().WithMessage("Crewlid ontbreekt id");
            crew.RuleFor(c => c.Name).NotNull().WithMessage("Geef een naam op").NotEmpty().WithMessage("Geef een naam op");
            crew.RuleFor(c => c.Type).NotNull().WithMessage("Geef een type op! (Actor of Director)").NotEmpty().WithMessage("Geef een type op! (Actor of Director)")
                .Must(t => conditions.Contains(t!))
                .WithMessage("Type moet Actor of Director zijn");
        });
    }
}