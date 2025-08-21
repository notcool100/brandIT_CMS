using BrandIT.Domain.Enums;

namespace BrandIT.Domain.Entities;

public class BlogPost : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? FeaturedImageUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public Guid AuthorId { get; set; }
    public User? Author { get; set; }
    public BlogPostStatus Status { get; set; } = BlogPostStatus.Draft;
    public DateTime? PublishedAt { get; set; }
    public int ReadTimeMinutes { get; set; } = 5;
    public int ViewCount { get; set; } = 0;
    public bool IsFeatured { get; set; } = false;
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
}
