using Microsoft.Extensions.Options;
using ReadFileFromBot.Models;

using ReadFileFromBot.Services.Interfaces;

namespace ReadFileFromBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly AppSettings _settings;
    private readonly IFileWatcherService _fileWatcherService;

    public Worker(
        ILogger<Worker> logger,
        IOptions<AppSettings> options,
        IFileWatcherService fileWatcherService)
    {
        _logger = logger;
        _settings = options.Value;
        _fileWatcherService = fileWatcherService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken){
        _logger.LogInformation(
            "ReadFileFromBot Service Started");

        CreateFolders();

        await _fileWatcherService.StartAsync(
            stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Worker Running : {Time}",
                DateTime.Now);

            await Task.Delay(
                TimeSpan.FromSeconds(5),
                stoppingToken);
        }
    }

    private void CreateFolders()
    {
        CreateFolder(_settings.InputPath);
        CreateFolder(_settings.WorkPath);
        CreateFolder(_settings.CompletePath);
        CreateFolder(_settings.ErrorPath);
        CreateFolder(_settings.LogPath);
        CreateFolder(_settings.XsdPath);
    }

    private void CreateFolder(string path)
    {
        Directory.CreateDirectory(path);

        _logger.LogInformation(
            "Create Folder : {Folder}",
            path);
    }
}