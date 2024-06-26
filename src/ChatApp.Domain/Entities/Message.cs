namespace ChatApp.Domain.Entities;

public class Message : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; }
    public virtual AppUser Sender { get; set; }
    public virtual Conversation Conversation { get; set; }
}
