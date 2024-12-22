using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.controllers;


[ApiController]
[Route("api/loan")]

public class KafkaController:ControllerBase
{
    private readonly KafkaService _kafkaService;

    public KafkaController(KafkaService kafkaService)
    {
        _kafkaService = kafkaService;
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyForLoan([FromBody] KafkaRequest request)
    {
        // Logjika për të përpunuar kërkesën për kredi

        // Dërgo mesazh në Kafka
        await _kafkaService.SendMessageAsync($"Loan application submitted: {request.ApplicationId}");

        return Ok("Loan application submitted");
    }
    public class KafkaRequest
    {
        public string ApplicationId { get; set; }
        // Shto fushat e tjera sipas nevojës për kredinë
    }
}