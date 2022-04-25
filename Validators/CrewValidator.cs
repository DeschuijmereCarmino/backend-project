namespace backendProject.API.Validators;

public class CrewValidator : AbstractValidator<Crew>
{
    List<string> conditions = new List<string>() { "Actor", "Director" };
    public CrewValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Geef een naam op").NotEmpty().WithMessage("Geef een naam op");
        RuleFor(c => c.Type).NotNull().WithMessage("Geef een type op! (Actor of Director)").NotEmpty().WithMessage("Geef een type op! (Actor of Director)")
            .Must(t => conditions.Contains(t!))
            .WithMessage("Type moet Actor of Director zijn");
    }
}