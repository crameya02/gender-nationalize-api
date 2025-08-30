using System.Text.Json.Serialization;

namespace Domain.Models
{

    public class NationalityResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }= string.Empty;

        [JsonPropertyName("country")]
        public List<CountryProbability> Country { get; set; }= new();
    }

    public class CountryProbability
    {
        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }= string.Empty;

        [JsonPropertyName("probability")]
        public double Probability { get; set; }
    }

}
