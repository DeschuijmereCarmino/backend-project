namespace backendProject.API.Validators;

public class CrewValidator : AbstractValidator<Crew>
{
    public CrewValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Geef een naam op");
        RuleFor(c => c.Type).NotEmpty().WithMessage("Geef een type op! (Actor of Director)");
    }
}