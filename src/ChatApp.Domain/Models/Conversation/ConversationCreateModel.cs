using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Models.Conversation
{
    public class ConversationCreateModel
    {
        [Required]
        public required string Name { get; set; }
    }
}
