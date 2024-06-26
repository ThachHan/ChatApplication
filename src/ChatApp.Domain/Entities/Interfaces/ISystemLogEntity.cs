namespace ChatApp.Domain.Entities.Interfaces
{
    public interface ISystemLogEntity
    {
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
