using System.Text.Json.Serialization;

namespace Domain.Models
{

    public class GenderResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }= string.Empty;

        [JsonPropertyName("gender")]
        public string Gender { get; set; }= string.Empty;

        [JsonPropertyName("probability")]
        public double Probability { get; set; }
    }

}