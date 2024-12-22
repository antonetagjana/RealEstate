using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

/*
namespace WebApplication2.controllers;

[ApiController]
[Route("api/[controller]")]

public class ExampleController  : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

   
    public ExampleController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    [HttpGet("fetch-data")]
    public async Task<IActionResult> FetchDataAsync()
    {
        
        var client = _httpClientFactory.CreateClient("OurWebAPI");

     
        var response = await client.GetAsync("data-endpoint"); 

        if (response.IsSuccessStatusCode)
        {
            
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data); 
        }

        return StatusCode((int)response.StatusCode, "Failed to fetch data");
    }
}
*/