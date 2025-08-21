using AutoMapper;
using BrandIT.Application.Common.Interfaces;
using BrandIT.Application.DTOs.Contact;
using BrandIT.Domain.Entities;
using BrandIT.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandIT.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ContactController> _logger;

    public ContactController(
        IApplicationDbContext context,
        IMapper mapper,
        ILogger<ContactController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ContactMessageDto>> CreateContactMessage([FromBody] CreateContactMessageRequest request)
    {
        try
        {
            var contactMessage = new ContactMessage
            {
                Name = request.Name,
                Email = request.Email,
                Company = request.Company,
                PhoneNumber = request.PhoneNumber,
                Subject = request.Subject,
                Message = request.Message,
                ServiceInterest = request.ServiceInterest,
                Status = ContactMessageStatus.New,
                Priority = ContactMessagePriority.Normal,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            var contactMessageDto = _mapper.Map<ContactMessageDto>(contactMessage);
            
            _logger.LogInformation("Contact message received from {Name} ({Email})", request.Name, request.Email);
            return CreatedAtAction(nameof(GetContactMessage), new { id = contactMessage.Id }, contactMessageDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating contact message");
            return StatusCode(500, new { message = "An error occurred while submitting your message. Please try again." });
        }
    }

    [HttpGet]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetContactMessages(
        [FromQuery] ContactMessageStatus? status = null,
        [FromQuery] ContactMessagePriority? priority = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var query = _context.ContactMessages
                .Include(cm => cm.AssignedToUser)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(cm => cm.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(cm => cm.Priority == priority.Value);

            var totalCount = await query.CountAsync();
            
            var messages = await query
                .OrderByDescending(cm => cm.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var messageDtos = _mapper.Map<List<ContactMessageDto>>(messages);

            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-Page-Size", pageSize.ToString());

            return Ok(messageDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact messages");
            return StatusCode(500, new { message = "An error occurred while retrieving messages" });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ContactMessageDto>> GetContactMessage(Guid id)
    {
        try
        {
            var message = await _context.ContactMessages
                .Include(cm => cm.AssignedToUser)
                .FirstOrDefaultAsync(cm => cm.Id == id);

            if (message == null)
                return NotFound(new { message = "Contact message not found" });

            var messageDto = _mapper.Map<ContactMessageDto>(message);
            return Ok(messageDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact message {MessageId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the message" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ContactMessageDto>> UpdateContactMessage(Guid id, [FromBody] UpdateContactMessageRequest request)
    {
        try
        {
            var message = await _context.ContactMessages
                .Include(cm => cm.AssignedToUser)
                .FirstOrDefaultAsync(cm => cm.Id == id);

            if (message == null)
                return NotFound(new { message = "Contact message not found" });

            message.Status = request.Status;
            message.Priority = request.Priority;
            message.AssignedToUserId = request.AssignedToUserId;
            message.InternalNotes = request.InternalNotes;
            message.UpdatedAt = DateTime.UtcNow;

            if (request.Status == ContactMessageStatus.Responded && message.ResponseSentAt == null)
            {
                message.ResponseSentAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            var messageDto = _mapper.Map<ContactMessageDto>(message);
            
            _logger.LogInformation("Contact message {MessageId} updated successfully", id);
            return Ok(messageDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact message {MessageId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the message" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteContactMessage(Guid id)
    {
        try
        {
            var message = await _context.ContactMessages
                .FirstOrDefaultAsync(cm => cm.Id == id);

            if (message == null)
                return NotFound(new { message = "Contact message not found" });

            // Soft delete
            message.IsDeleted = true;
            message.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact message {MessageId} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact message {MessageId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the message" });
        }
    }

    [HttpGet("stats")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<object>> GetContactStats()
    {
        try
        {
            var totalMessages = await _context.ContactMessages.CountAsync();
            var newMessages = await _context.ContactMessages.CountAsync(cm => cm.Status == ContactMessageStatus.New);
            var inProgressMessages = await _context.ContactMessages.CountAsync(cm => cm.Status == ContactMessageStatus.InProgress);
            var respondedMessages = await _context.ContactMessages.CountAsync(cm => cm.Status == ContactMessageStatus.Responded);
            var closedMessages = await _context.ContactMessages.CountAsync(cm => cm.Status == ContactMessageStatus.Closed);

            var todayMessages = await _context.ContactMessages
                .CountAsync(cm => cm.CreatedAt.Date == DateTime.UtcNow.Date);

            var thisWeekMessages = await _context.ContactMessages
                .CountAsync(cm => cm.CreatedAt >= DateTime.UtcNow.AddDays(-7));

            var stats = new
            {
                Total = totalMessages,
                New = newMessages,
                InProgress = inProgressMessages,
                Responded = respondedMessages,
                Closed = closedMessages,
                Today = todayMessages,
                ThisWeek = thisWeekMessages
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact stats");
            return StatusCode(500, new { message = "An error occurred while retrieving stats" });
        }
    }
}
