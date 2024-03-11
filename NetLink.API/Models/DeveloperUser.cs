namespace NetLink.API.Models
{
    public class DeveloperUser
    {
        public Guid Id { get; set; }
        public Guid DeveloperId { get; set; }

        public string? EndUserId { get; set; }

        public Developer? Developer { get; set; }
        public EndUser? EndUser { get; set; }
    }
}
