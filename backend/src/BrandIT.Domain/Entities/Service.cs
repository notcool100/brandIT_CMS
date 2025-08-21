namespace BrandIT.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public decimal? Price { get; set; }
    public string? PriceDescription { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; } = 0;
    public string Category { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
}
