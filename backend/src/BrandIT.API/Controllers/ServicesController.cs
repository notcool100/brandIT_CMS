using AutoMapper;
using BrandIT.Application.Common.Interfaces;
using BrandIT.Application.DTOs.Services;
using BrandIT.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandIT.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(
        IApplicationDbContext context,
        IMapper mapper,
        ILogger<ServicesController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
    {
        try
        {
            var services = await _context.Services
                .Where(s => s.IsActive)
                .OrderBy(s => s.SortOrder)
                .ThenBy(s => s.Name)
                .ToListAsync();

            var serviceDtos = _mapper.Map<List<ServiceDto>>(services);
            return Ok(serviceDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving services");
            return StatusCode(500, new { message = "An error occurred while retrieving services" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetService(Guid id)
    {
        try
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            var serviceDto = _mapper.Map<ServiceDto>(service);
            return Ok(serviceDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving service {ServiceId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the service" });
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<ServiceDto>> CreateService([FromBody] CreateServiceRequest request)
    {
        try
        {
            var service = new Service
            {
                Name = request.Name,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                Icon = request.Icon,
                Features = request.Features,
                Price = request.Price,
                PriceDescription = request.PriceDescription,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                SortOrder = request.SortOrder,
                Category = request.Category,
                Technologies = request.Technologies,
                CreatedAt = DateTime.UtcNow
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            var serviceDto = _mapper.Map<ServiceDto>(service);
            
            _logger.LogInformation("Service {ServiceName} created successfully", service.Name);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, serviceDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating service");
            return StatusCode(500, new { message = "An error occurred while creating the service" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<ServiceDto>> UpdateService(Guid id, [FromBody] UpdateServiceRequest request)
    {
        try
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            service.Name = request.Name;
            service.Description = request.Description;
            service.ShortDescription = request.ShortDescription;
            service.Icon = request.Icon;
            service.Features = request.Features;
            service.Price = request.Price;
            service.PriceDescription = request.PriceDescription;
            service.ImageUrl = request.ImageUrl;
            service.IsActive = request.IsActive;
            service.SortOrder = request.SortOrder;
            service.Category = request.Category;
            service.Technologies = request.Technologies;
            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var serviceDto = _mapper.Map<ServiceDto>(service);
            
            _logger.LogInformation("Service {ServiceName} updated successfully", service.Name);
            return Ok(serviceDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating service {ServiceId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the service" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        try
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            // Soft delete
            service.IsDeleted = true;
            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Service {ServiceName} deleted successfully", service.Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting service {ServiceId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the service" });
        }
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<string>>> GetServiceCategories()
    {
        try
        {
            var categories = await _context.Services
                .Where(s => s.IsActive && !string.IsNullOrEmpty(s.Category))
                .Select(s => s.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving service categories");
            return StatusCode(500, new { message = "An error occurred while retrieving categories" });
        }
    }
}
