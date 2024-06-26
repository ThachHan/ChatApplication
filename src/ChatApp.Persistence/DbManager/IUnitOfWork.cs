namespace ChatApp.Persistence.DbManager
{
    public interface IUnitOfWork
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task SaveAsync();
    }
}
