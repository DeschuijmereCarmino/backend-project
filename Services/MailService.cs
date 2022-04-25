namespace backendProject.API.Services;

public interface IMailService
{
    Task SendMailAsync(List<string> Emails, Movie movie);
}

public class MailService : IMailService
{
    private readonly MailConfig _config;

    public MailService(IOptions<MailConfig> config)
    {
        _config = config.Value;
    }

    public async Task SendMailAsync(List<string> emails, Movie movie)
    {

        foreach (var email in emails)
        {
            var client = new SendGridClient(_config.APIKey);

            var from = new EmailAddress("carmino.deschuijmere@student.howest.be", "Carmino");
            var subject = "New Release!";
            var to = new EmailAddress(email);

            var plainTextContent = $" {movie.Title} \n {movie.Description}";
           
            string htmlContent = string.Format(@"<html>
                                            <head >
                                                <title>{0}</title>
                                            </head>
                                            <body style='background-image: radial-gradient(
                                                    circle at 58% 50%,
                                                    hsla(89, 0%, 28%, 0.1) 0%,
                                                    hsla(89, 0%, 28%, 0.1) 60%,
                                                    transparent 60%,
                                                    transparent 98%,
                                                    transparent 98%,
                                                    transparent 100%
                                                ),
                                                radial-gradient(
                                                    circle at 36% 79%,
                                                    hsla(89, 0%, 28%, 0.1) 0%,
                                                    hsla(89, 0%, 28%, 0.1) 49%,
                                                    transparent 49%,
                                                    transparent 99%,
                                                    transparent 99%,
                                                    transparent 100%
                                                ),
                                                radial-gradient(
                                                    circle at 48% 23%,
                                                    hsla(89, 0%, 28%, 0.1) 0%,
                                                    hsla(89, 0%, 28%, 0.1) 50%,
                                                    transparent 50%,
                                                    transparent 87%,
                                                    transparent 87%,
                                                    transparent 100%
                                                ),
                                                radial-gradient(
                                                    circle at 55% 69%,
                                                    hsla(89, 0%, 28%, 0.1) 0%,
                                                    hsla(89, 0%, 28%, 0.1) 3%,
                                                    transparent 3%,
                                                    transparent 19%,
                                                    transparent 19%,
                                                    transparent 100%
                                                ),
                                                radial-gradient(
                                                    circle at 38% 69%,
                                                    hsla(89, 0%, 28%, 0.1) 0%,
                                                    hsla(89, 0%, 28%, 0.1) 15%,
                                                    transparent 15%,
                                                    transparent 33%,
                                                    transparent 33%,
                                                    transparent 100%
                                                ),
                                                linear-gradient(45deg, rgb(14, 124, 138), rgb(44, 254, 203));

                                                display: flex;
                                                align-items: center;
                                                justify-content: center;'>
                                                <div style='width: 90%;'>
                                                    <div>
                                                        <div style='margin: 30px; background-color: #ffffff; padding: 20px; padding-top: 2rem; filter: alpha(opacity=60); border-radius: 1rem;'>
                                                            <div>
                                                                <h1 style=' padding-top: 4%;text-align: center'>{0}</h1>
                                                                <p style = 'text-align: center' >{1}</p>
                                                                <p style='padding-top: 4%; text-align: right'>
                                                                <i>This is an automated email.Do not reply this email.</i>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </body>
                                        </html>", movie.Title, movie.Description, from.Email);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}