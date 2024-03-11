namespace NetLink.API.Models
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string? DevToken { get; set; } // Consider using byte array for tokens in production
        public string? Username { get; set; }

        public ICollection<DeveloperUser>? DeveloperUsers { get; set; }
    }
}
