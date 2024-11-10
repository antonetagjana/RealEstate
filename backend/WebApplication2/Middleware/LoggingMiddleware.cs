using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    // Konstruktori që merr _next middleware dhe ILogger për logim
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Metoda InvokeAsync e cila do të përpunojë kërkesat
    public async Task InvokeAsync(HttpContext context)
    {
        // Para se të kalojë te middleware-i tjetër, log kërkesën
        var requestLog = $"Request: {context.Request.Method} {context.Request.Path} at {DateTime.Now}";
        _logger.LogInformation(requestLog);

        // Ruaj log kërkesën në skedar
        await SaveLogToFile(requestLog);

        // Kalo kërkesën te middleware-i tjetër
        await _next(context);

        // Pasi të përpunojë përgjigjen, log statusin e përgjigjes
        var responseLog = $"Response: {context.Response.StatusCode} at {DateTime.Now}";
        _logger.LogInformation(responseLog);

        // Ruaj log përgjigjen në skedar
        await SaveLogToFile(responseLog);
    }

    // Metoda që ruan logun në një skedar
    private async Task SaveLogToFile(string message)
    {
        var logFilePath = "Logs/log.txt";  // Vendndodhja e skedarit log
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));  // Krijo dosjen Logs nëse nuk ekziston

        // Shto logun në skedar
        await File.AppendAllTextAsync(logFilePath, message + Environment.NewLine);
    }
}