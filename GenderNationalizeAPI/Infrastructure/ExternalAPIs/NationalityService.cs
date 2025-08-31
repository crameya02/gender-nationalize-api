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

        /// <summary>
        /// Constructor for NationalityService.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <remarks>
        /// The base URL and API key are retrieved from environment variables.
        /// </remarks>
        public NationalityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("NATIONALIZE_API") ?? "";;
            _apiKey = Environment.GetEnvironmentVariable("NATIONALIZE_API_KEY") ?? "";;
        }

        /// <summary>
        /// Gets the nationality of the given name from the Nationalize API.
        /// </summary>
        /// <param name="name">The name to get the nationality for.</param>
        /// <returns>The nationality of the given name, or a default result if the API fails.</returns>
        /// <remarks>
        /// The API key is used if it is set in the environment variables. The base URL
        /// is retrieved from the environment variables.
        /// </remarks>

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