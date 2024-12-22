using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class KafkaService
{
    private readonly HttpClient _httpClient;

    public KafkaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendMessageAsync(string message)
    {
        var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5098/api/kafka/send-synchronous", content);
        response.EnsureSuccessStatusCode();
    }
}