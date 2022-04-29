namespace backendProject.API.Validators;

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email).NotNull().WithMessage("Geef een email op").NotEmpty().WithMessage("Geef een email op").EmailAddress().WithMessage("Geef een geldig email op"); ;
        RuleFor(u => u.Password).NotNull().WithMessage("Geef een wachtwoord op").NotEmpty().WithMessage("Geef een wachtwoord op");
    }
}