namespace backendProject.API.Services;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMovieService _movieService;

    public Worker(ILogger<Worker> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //Elke minuut
            //await WaitForNextSchedule("* * * * *");
            //Elke dag om 13u
            await WaitForNextSchedule(" 0 13 * * *");
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    
            // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            // await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task WaitForNextSchedule(string cronExpression)
    {
        var parsedExp = CronExpression.Parse(cronExpression);
        var currentUtcTime = DateTimeOffset.UtcNow.UtcDateTime;
        var occurenceTime = parsedExp.GetNextOccurrence(currentUtcTime);

        var delay = occurenceTime.GetValueOrDefault() - currentUtcTime;
        _logger.LogInformation("The run is delayed for {delay}. Current time: {time}", delay, DateTimeOffset.Now);
        await _movieService.SendMailAsync();
        _logger.LogInformation("Email sent");

        await Task.Delay(delay);
    }
}