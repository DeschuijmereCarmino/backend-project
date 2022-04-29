namespace backendProject.API.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    List<string> conditions = new List<string>() { "Actor", "Director" };
    public MovieValidator()
    {
        RuleFor(m => m.Title).NotNull().WithMessage("Geef een titel op").NotEmpty().WithMessage("Geef een titel op");
        RuleFor(m => m.Description).NotNull().WithMessage("Geef een omschrijving op").NotEmpty().WithMessage("Geef een omschrijving op");
        RuleFor(m => m.ReleaseDate).NotNull().WithMessage("Geef een release date op").NotEmpty().WithMessage("Geef een release date op").Matches(@"^(((0[1-9]|[12][0-9]|3[01])[/](0[13578]|1[02])|(0[1-9]|[12][0-9]|30)[/](0[469]|11)|(0[1-9]|1\d|2[0-8])[/]02)[/]\d{4}|29[- /.]02[/](\d{2}(0[48]|[2468][048]|[13579][26])|([02468][048]|[1359][26])00))$").WithMessage("release date moet dd/mm/yyyy zijn");

        RuleFor(m => m.Crew).NotNull().WithMessage("Geef minstens 1 crewlid op").NotEmpty().WithMessage("Geef minstens 1 crewlid op");

        When(u => u.Crew != null, () =>
        {
            RuleForEach(m => m.Crew).ChildRules(crew =>
            {
                crew.RuleFor(c => c.Id).NotEmpty().WithMessage("Crewlid ontbreekt id").NotNull().WithMessage("Crewlid ontbreekt id");
                crew.RuleFor(c => c.Name).NotNull().WithMessage("Geef een naam op").NotEmpty().WithMessage("Geef een naam op");
                crew.RuleFor(c => c.Type).NotNull().WithMessage("Geef een type op! (Actor of Director)").NotEmpty().WithMessage("Geef een type op! (Actor of Director)")
                    .Must(t => conditions.Contains(t!))
                    .WithMessage("Type moet Actor of Director zijn");
            });
        });
    }
}