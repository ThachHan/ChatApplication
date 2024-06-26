using ChatApp.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Entities;

public class BaseEntity : ISystemLogEntity, ISoftDelete
{
    [Key]
    public Guid Id { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedAt { get; set; }
}
