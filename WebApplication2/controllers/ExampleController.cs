using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebApplication2.controllers;

[ApiController]
[Route("api/[controller]")]

public class ExampleController  : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    // Injektimi i IHttpClientFactory përmes konstruktorit
    public ExampleController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Krijo një metodë që bën kërkesë GET për një endpoint të API-së
    [HttpGet("fetch-data")]
    public async Task<IActionResult> FetchDataAsync()
    {
        // Krijo një HttpClient duke përdorur emrin "OurWebAPI" (i konfiguruar në Program.cs)
        var client = _httpClientFactory.CreateClient("OurWebAPI");

        // Bëj një kërkesë GET për një endpoint specifik
        var response = await client.GetAsync("data-endpoint"); // Ndrysho "data-endpoint" sipas nevojës

        if (response.IsSuccessStatusCode)
        {
            // Lexo përmbajtjen e përgjigjes
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data); // Kthe të dhënat në përgjigje
        }

        return StatusCode((int)response.StatusCode, "Failed to fetch data");
    }
}
