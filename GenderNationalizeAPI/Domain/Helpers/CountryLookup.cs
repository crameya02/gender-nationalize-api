using System.Text.Json;

namespace Domain.Helpers

{
    public static class CountryLookup
    {
        private static Dictionary<string, string> CountryNames = new();
        
        public static void LoadFromFile(string path)
        {
            
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                CountryNames = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            }
            else
            {
                Console.WriteLine($"File not found: {path}");   
            }
        }

        public static string GetName(string countryId) =>
            CountryNames.TryGetValue(countryId, out var name) ? name : countryId;
    }
}