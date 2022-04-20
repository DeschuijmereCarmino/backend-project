namespace backendProject.API.Services;

public class MailService
{
    public async Task SendMailAsync(List<string> Emails)
    {
        List<string> emails = new List<string>();

        // foreach (var email in emails)
        // {

        var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("test@example.com", "Example User");
        var subject = "New Release!";
        var to = new EmailAddress("test@example.com", "Example User");
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        // }

    }
}