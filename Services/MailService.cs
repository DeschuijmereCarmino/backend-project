namespace backendProject.API.Services;

public class MailService
{
    private readonly MailConfig _config;

    public MailService(IOptions<MailConfig> config)
    {
        _config = config.Value;
    }

    public async Task SendMailAsync(List<string> Emails, Movie movie)
    {

        List<string> emails = new List<string>();

        foreach (var email in emails)
        {
            var client = new SendGridClient(_config.APIKey);

            var from = new EmailAddress("carmino.deschuijmere@student.howest.be", "Carmino");
            var subject = "New Release!";
            var to = new EmailAddress("carmino.deschuijmere@student.howest.be");

            var plainTextContent = $" {movie.Title} \n {movie.Description}";
            string htmlContent;
            // htmlContent = String.Format(@"<html>
            //                         <head>
            //                             <style>body{font-family: arial; font-size: 10pt; background: url('https://images.pexels.com/photos/850796/pexels-photo-850796.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940');}
            //                             img{display: block;  width: 300px; margin: auto; margin-bottom: 5%;}
            //                             h2{font-family:arial; font-size: 10pt; padding-left: 10px; color: #383838;}
            //                             li{line-height: 1em; margin-bottom: 12px;}
            //                             span{color: blue;}
            //                             .wrapper{width: 90%; margin: 20px auto; }
            //                             .end { padding-top:4%; }
            //                             p .box { padding-top:5%; }
            //                             .box {margin: 30px;
            //                                 background-color: #ffffff;
            //                                 border: 1px solid black;
            //                                 opacity: 0.8;
            //                                 padding: 20px;
            //                                 filter: alpha(opacity=60);}
            //                             .box-bottom {
            //                                 padding-left: 40px;
            //                                 padding-right: 40px;}
            //                             @media screen and (min-width: 600px) {
            //                                 .wrapper{width: 60%; margin: 20px auto;} 
            //                                 p .box{
            //                                     padding-top:2%;
            //                                 }
            //                                 .end {
            //                                     padding-top:4%;
            //                                 }
            //                             }
            //                             </style>
            //                         </head>
            //                         <body>
            //                             <div class='wrapper'>
            //                                 < div class='box'>
            //                                         <img src = 'https://media.istockphoto.com/vectors/new-release-round-isolated-ribbon-label-new-release-sign-vector-id1271265483?k=20&m=1271265483&s=612x612&w=0&h=BXbM8LhpYRJhJ9PqFouEw9xzTJOnTCsXfaSqMFvFrzw=' alt='Logo'>
            //                                         <div>
            //                                             <h4 class='end' style='text-align: left;'>Hi Inne Prinusantari,</h4><p> Thanks for register and welcome to the jungle!</p><p>Your account has been successfully added and you can now login to our App and start exploring.Click<a href='#'> here</a> to verify your Email.</p>
            //                                             <p class='end'><i>This is an automated email.Do not reply this email.</i></p>
            //                                         </div>
            //                                 </div>
            //                                 <div class='box-bottom'>
            //                                     <p>If you have questions, please do not hesitate to contact us through direct message in the App or email to<a href='mailto:{0}'> {0}</a>.</p>
            //                                     <p>© Code in 2022</p>
            //                                 </div>
            //                             </div>
            //                         </body>
            //                     </html>", from.Email);

            htmlContent = String.Format(@"<html>
                                            <head>
                                                <style>
                                                body {
                                                    font-family: arial;
                                                    font-size: 10pt;
                                                    background-image: radial-gradient(
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
                                                    justify-content: center;
                                                }
                                                img {
                                                    display: block;
                                                    width: 300px;
                                                    margin: auto;
                                                    margin-bottom: 5%;
                                                }
                                                h2 {
                                                    font-family: arial;
                                                    font-size: 10pt;
                                                    padding-left: 10px;
                                                    color: #383838;
                                                }
                                                li {
                                                    line-height: 1em;
                                                    margin-bottom: 12px;
                                                }
                                                span {
                                                    color: blue;
                                                }
                                                .wrapper {
                                                    width: 90%;
                                                }
                                                .end {
                                                    padding-top: 4%;
                                                }
                                                p .box {
                                                    padding-top: 5%;
                                                }
                                                .box {
                                                    margin: 30px;
                                                    background-color: #ffffff;
                                                    padding: 20px;
                                                    padding-top: 2rem;
                                                    filter: alpha(opacity=60);
                                                    border-radius: 1rem;
                                                }
                                                .box-bottom {
                                                    padding-left: 40px;
                                                    padding-right: 40px;
                                                }
                                                @media screen and (min-width: 600px) {
                                                    .wrapper {
                                                    width: 40%;
                                                    }
                                                    p .box {
                                                    padding-top: 2%;
                                                    }
                                                    .end {
                                                    padding-top: 4%;
                                                    }
                                                }
                                                </style>
                                            </head>
                                            <body>
                                                <div class='wrapper'>
                                                <div>
                                                    <div class='box'>
                                                    <img
                                                        src = 'https://media.istockphoto.com/vectors/new-release-round-isolated-ribbon-label-new-release-sign-vector-id1271265483?k=20&m=1271265483&s=612x612&w=0&h=BXbM8LhpYRJhJ9PqFouEw9xzTJOnTCsXfaSqMFvFrzw='
                                                        alt='Logo'
                                                    />
                                                    <div>
                                                        <h1 class='end' style='text-align: center'>{0}</h1>
                                                        <p style ='text-align: center'>{1}</p>
                                                        <p class='end' style='text-align: right'>
                                                        <i>This is an automated email.Do not reply this email.</i>
                                                        </p>
                                                    </div>
                                                    </div>
                                                    <div class='box-bottom'>
                                                    <p style='color: #ffffff'>
                                                        If you have questions, please do not hesitate to contact us through
                                                        email to<a href='mailto:{2}' style='color: #ffffff;'> {2}</a>.
                                                    </p>
                                                    <p style='color: #ffffff'>© Code in 2022</p>
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