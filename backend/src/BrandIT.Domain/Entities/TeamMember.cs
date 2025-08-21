namespace BrandIT.Domain.Entities;

public class TeamMember : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public List<string> Skills { get; set; } = new();
    public List<string> Certifications { get; set; } = new();
    public DateTime? JoinedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; } = 0;

    public string FullName => $"{FirstName} {LastName}";
}
