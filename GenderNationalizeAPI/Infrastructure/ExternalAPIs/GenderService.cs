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

        /// <summary>
        /// Constructor for the GenderService class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for making API requests.</param>
        /// <remarks>
        /// The <c>GENDERIZE_API</c> and <c>GENDERIZE_API_KEY</c> environment variables
        /// are used to configure the API endpoint and API key, respectively.
        /// </remarks>
        public GenderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("GENDERIZE_API") ?? "";
            _apiKey = Environment.GetEnvironmentVariable("GENDERIZE_API_KEY") ?? "";
        }


        /// <summary>
        /// Retrieves gender information for the given name from the Genderize API.
        /// </summary>
        /// <param name="name">The name to retrieve gender information for.</param>
        /// <returns>
        /// A <see cref="GenderResult"/> containing information about the gender of the given name.
        /// </returns>
        /// <remarks>
        /// The <c>GENDERIZE_API</c> and <c>GENDERIZE_API_KEY</c> environment variables
        /// are used to configure the API endpoint and API key, respectively.
        /// </remarks>
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