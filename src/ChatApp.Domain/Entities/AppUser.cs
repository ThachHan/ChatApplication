using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    [Key]
    public override Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public virtual ICollection<Message> MessageSends { get; set; }
    public virtual ICollection<Conversation> ConversationCreateds { get; set; }
    public virtual ICollection<Participant> Participants { get; set; }

}
