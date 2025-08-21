using BrandIT.Domain.Enums;

namespace BrandIT.Application.DTOs.Contact;

public class ContactMessageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? PhoneNumber { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? ServiceInterest { get; set; }
    public ContactMessageStatus Status { get; set; }
    public ContactMessagePriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? AssignedToUser { get; set; }
    public DateTime? ResponseSentAt { get; set; }
    public string? InternalNotes { get; set; }
}

public class CreateContactMessageRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? PhoneNumber { get; set; }
    public string Subject { get; set; } = "General Inquiry";
    public string Message { get; set; } = string.Empty;
    public string? ServiceInterest { get; set; }
}

public class UpdateContactMessageRequest
{
    public ContactMessageStatus Status { get; set; }
    public ContactMessagePriority Priority { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public string? InternalNotes { get; set; }
}
