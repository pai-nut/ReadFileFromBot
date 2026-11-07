using ReadFileFromBot.Services.Interfaces;

namespace ReadFileFromBot.Services;

public class FileWatcherService : IFileWatcherService
{
    private readonly ILogger<FileWatcherService> _logger;

    public FileWatcherService(
        ILogger<FileWatcherService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "FileWatcherService Started");

        return Task.CompletedTask;
    }
}