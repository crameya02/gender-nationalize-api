namespace Domain.Models
{
    public class ProfileResult
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public List<string> Nationality { get; set; } = new();
    }

}