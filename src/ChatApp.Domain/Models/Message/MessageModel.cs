using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Models.Message;

public class MessageModel
{
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string FromUserName { get; set; } = string.Empty;
    [Required]
    public string Room { get; set; } = string.Empty;
}
