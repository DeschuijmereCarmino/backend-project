namespace backendProject.API.Validators;

public class UserValidator : AbstractValidator<User>
{
    List<string> conditions = new List<string>() { "Actor", "Director" };
    public UserValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Geef een username op");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Geef een wachtwoord op");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Geef een email op").EmailAddress().WithMessage("Geef een geldig email op");

        When(u => u.Movies != null, () =>
        {
            RuleForEach(u => u.Movies).ChildRules(movie =>
            {
                movie.RuleFor(m => m.Id).NotNull().WithMessage("Geef een id op").NotEmpty().WithMessage("Geef een id op");
                movie.RuleFor(m => m.Title).NotNull().WithMessage("Geef een titel op").NotEmpty().WithMessage("Geef een titel op");
            });
        });

        When(u => u.Crew != null, () =>
        {
            RuleForEach(u => u.Crew).ChildRules(crew =>
            {

                crew.RuleFor(c => c.Id).NotNull().WithMessage("Crewlid ontbreekt id").NotEmpty().WithMessage("Crewlid ontbreekt id");
                crew.RuleFor(c => c.Name).NotNull().WithMessage("Geef een naam op").NotEmpty().WithMessage("Geef een naam op");
                crew.RuleFor(c => c.Type).NotNull().WithMessage("Geef een type op! (Actor of Director)").NotEmpty().WithMessage("Geef een type op! (Actor of Director)")
                    .Must(t => conditions.Contains(t!))
                    .WithMessage("Type moet Actor of Director zijn");
            });
        });
    }
}