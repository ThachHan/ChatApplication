namespace ChatApp.Domain.Entities;

public class Participant : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public virtual Conversation Conversation { get; set; }
}
