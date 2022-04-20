namespace backendProject.API.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Geef een username op");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Geef een wachtwoord op");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Geef een email op");
    }
}