namespace ReadFileFromBot.Services.Interfaces;

public interface IFileWatcherService
{
    Task StartAsync(
        CancellationToken cancellationToken);
}