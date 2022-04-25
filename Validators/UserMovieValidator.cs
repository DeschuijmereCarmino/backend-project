namespace backendProject.API.Validators;

public class UserMovieValidator : AbstractValidator<UserMovie>
{
    public UserMovieValidator()
    {
        RuleFor(m => m.Id).NotNull().WithMessage("Geef een id op").NotEmpty().WithMessage("Geef een id op");
        RuleFor(m => m.Title).NotNull().WithMessage("Geef een titel op").NotEmpty().WithMessage("Geef een titel op");
    }
}