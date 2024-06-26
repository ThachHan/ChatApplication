namespace ChatApp.Domain.Entities.Interfaces;

public interface IAuditEntity
{
    public Guid? CreatedUserId { get; set; }
    public Guid? UpdatedUserId { get; set; }

}
