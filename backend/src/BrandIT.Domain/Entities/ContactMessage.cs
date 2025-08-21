using BrandIT.Domain.Enums;

namespace BrandIT.Domain.Entities;

public class ContactMessage : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? PhoneNumber { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? ServiceInterest { get; set; }
    public ContactMessageStatus Status { get; set; } = ContactMessageStatus.New;
    public Guid? AssignedToUserId { get; set; }
    public User? AssignedToUser { get; set; }
    public DateTime? ResponseSentAt { get; set; }
    public string? InternalNotes { get; set; }
    public ContactMessagePriority Priority { get; set; } = ContactMessagePriority.Normal;
}
