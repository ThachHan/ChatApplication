using System.Collections.ObjectModel;

namespace ChatApp.Domain.Entities;

public class Conversation : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid CreatorId { get; set; }
    public virtual AppUser Creator { get; set; }
   
    public virtual Collection<Message> Messages { get; set; }
    public virtual Collection<Participant> Participants { get; set; }
}
