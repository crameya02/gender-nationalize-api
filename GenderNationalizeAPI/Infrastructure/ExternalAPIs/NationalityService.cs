using Application.Services;
using Domain.Models;
using System.Text.Json;
namespace Infrastructure.ExternalAPIs
{
    public class NationalityService : INationalityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public NationalityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("NATIONALIZE_API") ?? "";;
            _apiKey = Environment.GetEnvironmentVariable("NATIONALIZE_API_KEY") ?? "";;
        }

        public async Task<NationalityResult> GetNationalityAsync(string name)
        {
            var url = string.IsNullOrEmpty(_apiKey)
                ? $"{_baseUrl}?name={name}"
                : $"{_baseUrl}?name={name}&apikey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Nationality API Response: " + json);
            
            return JsonSerializer.Deserialize<NationalityResult>(json) ?? new NationalityResult();
        }
    }
}