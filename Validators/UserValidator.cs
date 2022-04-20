namespace backendProject.API.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Geef een username op");
    }
}