using System.Text.Json;
using Application.Services;
using Domain.Models;

namespace Infrastructure.ExternalAPIs
{
    public class GenderService : IGenderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public GenderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("GENDERIZE_API") ?? "";
            _apiKey = Environment.GetEnvironmentVariable("GENDERIZE_API_KEY") ?? "";
        }


        public async Task<GenderResult> GetGenderAsync(string name)
        {
            var url = string.IsNullOrEmpty(_apiKey)
                ? $"{_baseUrl}?name={name}"
                : $"{_baseUrl}?name={name}&apikey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Gender API Response: " + json);

            return JsonSerializer.Deserialize<GenderResult>(json) ?? new GenderResult();

        }
    }
}