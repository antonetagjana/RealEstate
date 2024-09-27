using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

/*
namespace WebApplication2.Middleware;

public class RequestLoggingMiddleware
{
  private readonly RequestDelegate _next;

  public RequestLoggingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    var request = context.Request;
    var headers = request.Headers;


    var logEntry = $"Kerkesa {request.Method}{request.Path} ne {System.DateTime.Now}\n";
    foreach (var header in headers)
    {
      logEntry += $"{header.Key} : {header.Value}\n";
    }

    await File.AppendAllTextAsync("Logs/request_log.txt", logEntry);

    await _next(context);
  }
}*/