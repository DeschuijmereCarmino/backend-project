namespace backendProject.API.Services;

public interface IMovieService
{
    Task SetupData();
    Task<List<Crew>> GetActorCrewAsync();
    Task<List<Crew>> GetCrewAsync();
    Task<List<Crew>> GetDirectorCrewAsync();
    Task<string> GetIdByCrewMemberNameAsync(string name);
    Task<Movie> AddMovieAsync(Movie movie);
    Task<List<Movie>> AddMoviesAsync(List<Movie> movie);
    Task<Movie> GetMovieAsync(string id);
    Task<List<Movie>> GetMoviesAsync();
    Task SendMailAsync();
    Task<User> AddUserAsync(User newUser);
    Task<List<User>> AddUsersAsync(List<User> newUsers);
    Task<User> GetUserAsync(string id);
    Task<List<User>> GetUsersAsync();
    Task<List<string>> GetEmailAsync();
}

public class MovieService : IMovieService
{
    private readonly ICrewRepository _crewRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IUserRepository _userRepository;

    public MovieService(ICrewRepository crewRepository, IMovieRepository movieRepository, IUserRepository userRepository)
    {
        _crewRepository = crewRepository;
        _movieRepository = movieRepository;
        _userRepository = userRepository;
    }

    public async Task SetupData()
    {
        try
        {
            if (!(await _crewRepository.GetCrewAsync()).Any())
                await _crewRepository.AddCrewAsync(new List<Crew>() {
                    new Crew() {
                                Name = "Harrison Ford",
                                Type = "Actor" },
                    new Crew() {
                                Name = "Robert Pattinson",
                                Type = "Actor" },
                    new Crew() {
                                Name = "Viggo Mortensen",
                                Type = "Actor" },
                    new Crew() {
                                Name = "Micheal J. Fox",
                                Type = "Actor" },
                    new Crew() {
                                Name = "Mark Hamill",
                                Type = "Actor" },
                    new Crew() {
                                Name = "Oscar Isaac",
                                Type = "Actor"},
                    new Crew() {
                                Name = "Denis Villeneuve",
                                Type = "Director"},
                    new Crew() {
                                Name = "George Lucas",
                                Type = "Director"},
                    new Crew() {
                                Name = "Matt Reeves",
                                Type = "Director"},
                    new Crew() {
                                Name = "Peter Jackson",
                                Type = "Director"},
                    new Crew() {
                                Name = "Steven Spielberg",
                                Type = "Director"},
                    new Crew() {
                                Name = "Gary Trousdale",
                                Type = "Director"}
                }
            );

            string Harrison = await _crewRepository.GetIdByCrewMemberNameAsync("Harrison Ford");
            string Robert = await _crewRepository.GetIdByCrewMemberNameAsync("Robert Pattinson");
            string Viggo = await _crewRepository.GetIdByCrewMemberNameAsync("Viggo Mortensen");
            string Micheal = await _crewRepository.GetIdByCrewMemberNameAsync("Micheal J. Fox");
            string Mark = await _crewRepository.GetIdByCrewMemberNameAsync("Mark Hamill");
            string Oscar = await _crewRepository.GetIdByCrewMemberNameAsync("Oscar Isaac");

            string Denis = await _crewRepository.GetIdByCrewMemberNameAsync("Denis Villeneuve");
            string George = await _crewRepository.GetIdByCrewMemberNameAsync("George Lucas");
            string Matt = await _crewRepository.GetIdByCrewMemberNameAsync("Matt Reeves");
            string Peter = await _crewRepository.GetIdByCrewMemberNameAsync("Peter Jackson");
            string Steven = await _crewRepository.GetIdByCrewMemberNameAsync("Steven Spielberg");
            string Gary = await _crewRepository.GetIdByCrewMemberNameAsync("Gary Trousdale");

            if (!(await _movieRepository.GetMoviesAsync()).Any())
                await _movieRepository.AddMoviesAsync(new List<Movie>() {
                    new Movie() {
                        Title = "Dune",
                        Description = "Paul Atreides, a brilliant and gifted young man born into a great destiny beyond his understanding, must travel to the most dangerous planet in the universe to ensure the future of his family and his people. As malevolent forces explode into conflict over the planet’s exclusive supply of the most precious resource in existence-a commodity capable of unlocking humanity’s greatest potential-only those who can conquer their fear will survive.",
                        ReleaseDate = "22/10/2021",
                        Crew = new List<Crew>(){
                            new Crew()
                            {
                                Id = Denis,
                                Name = "Denis Villeneuve",
                                Type = "Director"
                            },
                            new Crew()
                            {
                                Id = Oscar,
                                Name = "Oscar Isaac",
                                Type = "Actor"
                            }
                        }
                    },
                    new Movie() {
                        Title = "Star Wars",
                        Description = "Princess Leia is captured and held hostage by the evil Imperial forces in their effort to take over the galactic Empire. Venturesome Luke Skywalker and dashing captain Han Solo team together with the loveable robot duo R2-D2 and C-3PO to rescue the beautiful princess and restore peace and justice in the Empire.",
                        ReleaseDate = "25/05/1977",
                        Crew = new List<Crew>(){
                            new Crew()
                            {
                                Id = George,
                                Name = "George Lucas",
                                Type = "Director"
                            },
                            new Crew()
                            {
                                Id = Harrison,
                                Name = "Harrison Ford",
                                Type = "Actor"
                            },
                            new Crew()
                            {
                                Id = Mark,
                                Name = "Mark Hamill",
                                Type = "Actor"
                            }
                        } },
                    new Movie() {
                        Title = "The Batman",
                        Description = "In his second year of fighting crime, Batman uncovers corruption in Gotham City that connects to his own family while facing a serial killer known as the Riddler.",
                        ReleaseDate = "04/03/2022",
                        Crew = new List<Crew>(){
                        new Crew()
                            {
                                Id = Matt,
                                Name = "Matt Reeves",
                                Type = "Director"
                            },
                        new Crew()
                            {
                                Id = Robert,
                                Name = "Robert Pattinson",
                                Type = "Actor"
                            }
                        }
                    },
                    new Movie() {
                        Title = "Atlantis: The Lost Empire",
                        Description = "The world’s most highly qualified crew of archaeologists and explorers is led by historian Milo Thatch as they board the incredible 1,000-foot submarine Ulysses and head deep into the mysteries of the sea. The underwater expedition takes an unexpected turn when the team’s mission must switch from exploring Atlantis to protecting it.",
                        ReleaseDate = "15/06/2001",
                        Crew = new List<Crew>(){
                        new Crew()
                            {
                                Id = Gary,
                                Name = "Gary Trousdale",
                                Type = "Director"
                            },
                        new Crew()
                            {
                                Id = Micheal,
                                Name = "Micheal J. Fox",
                                Type = "Actor"
                            }
                        }
                    },
                    new Movie() {
                        Title = "The Lord of the Rings: The Return of the King",
                        Description = "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron’s forces. Meanwhile, Frodo and Sam take the ring closer to the heart of Mordor, the dark lord’s realm.",
                        ReleaseDate = "17/12/2003",
                        Crew = new List<Crew>(){
                        new Crew()
                            {
                                Id = Peter,
                                Name = "Peter Jackson",
                                Type = "Director"
                            },
                        new Crew()
                            {
                                Id = Viggo,
                                Name = "Viggo Mortensen",
                                Type = "Actor"
                            }
                        }
                    },
                    new Movie() {
                        Title = "Indiana Jones and the Last Crusade",
                        Description = "When Dr. Henry Jones Sr. suddenly goes missing while pursuing the Holy Grail, eminent archaeologist Indiana must team up with Marcus Brody, Sallah and Elsa Schneider to follow in his father’s footsteps and stop the Nazis from recovering the power of eternal life.",
                        ReleaseDate = "24/05/1989",
                        Crew = new List<Crew>(){
                        new Crew()
                            {
                                Id = Steven,
                                Name = "Steven Spielberg",
                                Type = "Director"
                            },
                        new Crew()
                            {
                                Id = Harrison,
                                Name = "Harrison Ford",
                                Type = "Actor"
                            }
                        }
                    }
                }
            );
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Crew> GetCrewMemberAsync(string id) => await _crewRepository.GetCrewMemberAsync(id);

    public async Task<List<Crew>> GetActorCrewAsync() => await _crewRepository.GetActorCrewAsync();

    public async Task<List<Crew>> GetCrewAsync() => await _crewRepository.GetCrewAsync();

    public async Task<List<Crew>> GetDirectorCrewAsync() => await _crewRepository.GetDirectorCrewAsync();

    public async Task<string> GetIdByCrewMemberNameAsync(string name) => await _crewRepository.GetIdByCrewMemberNameAsync(name);

    public async Task<Movie> AddMovieAsync(Movie movie) => await _movieRepository.AddMovieAsync(movie);
    public async Task<List<Movie>> AddMoviesAsync(List<Movie> movies) => await _movieRepository.AddMoviesAsync(movies);

    public async Task<Movie> GetMovieAsync(string id) => await _movieRepository.GetMovieAsync(id);

    public async Task<List<Movie>> GetMoviesAsync() => await _movieRepository.GetMoviesAsync();

    public async Task SendMailAsync()
    {
        List<string> emails = new List<string>();

        var movies = await GetMoviesAsync();
        var today = DateTime.Today.ToString("d");

        foreach (var movie in movies)
        {
            if (movie.ReleaseDate == today)
            {
                var users = await GetUsersAsync();
                foreach (var user in users)
                {
                    foreach (var userMovie in user.Movies!)
                    {
                        //if film in user => add email to emails
                        if (userMovie == movie)
                        {
                            //if crew of film in user and not in emails yet => add email to emails
                            if (!emails.Contains(user.Email!))
                            {
                                emails.Add(user.Email!);
                            }
                        }
                    }
                }

            }
        }
    }

    public async Task<User> AddUserAsync(User newUser) => await _userRepository.AddUserAsync(newUser);

    public async Task<List<User>> AddUsersAsync(List<User> newUsers) => await _userRepository.AddUsersAsync(newUsers);

    public async Task<User> GetUserAsync(string id) => await _userRepository.GetUserAsync(id);

    public async Task<List<User>> GetUsersAsync() => await _userRepository.GetUsersAsync();

    public async Task<List<string>> GetEmailAsync() => await _userRepository.GetEmailsAsync();

    //for movie in movies
    // if (movie.releaseDate == currentDate)
    // {

    //   var emails = []
    //   foreach(guid in movie.users)
    //   {  
    //     emails.add(getUsersEmailsById)
    //   }

    //   foreach(guid in movie.directors)
    //   {
    //      var ids = []
    //      foreach id in ids
    //      {
    //         emails.add(getUsersEmailsFromDirectorsId)
    //      }
    //   }

    //   foreach(guid in movie.directors)
    //   {
    //      var ids = []
    //      foreach id in ids
    //      {
    //        emails.add(getUsersEmailsFromActorsId)  
    //      }
    //   }
    //   foreach email in email => send email notifying of release
    // } 

}