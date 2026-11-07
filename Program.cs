using ReadFileFromBot;
using ReadFileFromBot.Models;
using ReadFileFromBot.Services;
using ReadFileFromBot.Services.Interfaces;
using Serilog;

var logPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "data_log",
    $"log-{DateTime.Now:yyyy-MM-dd}.log");

Directory.CreateDirectory(
    Path.GetDirectoryName(logPath)!);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        logPath,
        rollingInterval: RollingInterval.Day,
        shared: true)
    .CreateLogger();

Log.Information("=== Serilog Started ===");

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("FolderSettings"));

builder.Services.AddSingleton<IFileWatcherService, FileWatcherService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();