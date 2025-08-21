namespace BrandIT.Application.DTOs.Services;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public decimal? Price { get; set; }
    public string? PriceDescription { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateServiceRequest
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

public class UpdateServiceRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public decimal? Price { get; set; }
    public string? PriceDescription { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
}
