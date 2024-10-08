using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]);
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        var response = await client.SendEmailAsync(msg);
    }

    public async Task SendConfirmationEmailAsync(string toEmail, string userId, string token)
    {
        var confirmationLink =
            $"http://localhost:5000/index.html/Account/ConfirmEmail?userId={userId}&token={Uri.EscapeDataString(token)}";
        var subject = "Confirm your email";
        var message = $"Please confirm your email by clicking on the following link: <a href='{confirmationLink}'>Confirm Email</a>";
            
        await SendEmailAsync(toEmail, subject, message);
    }
}