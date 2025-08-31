using System.Text.Json;

namespace Domain.Helpers

{
    public static class CountryLookup
    {
        private static Dictionary<string, string> CountryNames = new();
        
        /// <summary>
        /// Loads a JSON file containing country codes and names into memory.
        /// If the file does not exist, prints a message to the console.
        /// </summary>
        /// <param name="path">Path to the JSON file.</param>
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