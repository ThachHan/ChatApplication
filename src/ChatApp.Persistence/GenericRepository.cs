using ChatApp.Domain.Entities;
using ChatApp.Persistence.DbManager;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAllAsync();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<List<TEntity>?> GetByIdsAsync(List<Guid> ids);

    Task<TEntity> CreateAsync(TEntity entity);

    Task<List<TEntity>> CreateAsync(List<TEntity> entities);

    void Update(TEntity entity);

    void Update(List<TEntity> entities);

    void Delete(TEntity entity);

    void Delete(List<TEntity> entities);

    Task SaveChangesAsync();

    Task<bool> IsExistAsync(Guid id);
}

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext DBContext;
    protected readonly DbSet<TEntity> Entities;

    public GenericRepository(AppDbContext dbContext)
    {
        DBContext = dbContext;
        Entities = DBContext.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAllAsync()
    {
        return Entities.AsNoTracking().Where(n => !n.IsDeleted);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await Entities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    public virtual async Task<List<TEntity>?> GetByIdsAsync(List<Guid> ids)
    {
        return await GetAllAsync()
            .Where(n => ids.Contains(n.Id))
            .ToListAsync();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = entity.UpdatedAt = DateTime.Now;
        await Entities.AddAsync(entity);

        return entity;
    }

    public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = entity.UpdatedAt = DateTime.Now;
        }
        await Entities.AddRangeAsync(entities);

        return entities;
    }

    public virtual void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        Entities.Update(entity);
    }

    public virtual void Update(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        Entities.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public void Delete(List<TEntity> entities)
    {
        Entities.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await DBContext.SaveChangesAsync();
    }

    public Task<bool> IsExistAsync(Guid id)
    {
        return Entities
                    .AsNoTracking()
                    .AnyAsync(e => e.Id == id && !e.IsDeleted);
    }
}
