using System.Linq.Expressions;
using DataWise.Data.DbContexts.Releational;
using Microsoft.EntityFrameworkCore;

namespace DataWise.Data.Repositories.Releational;

public class SQLRepository<TType, TId>(
    UserDbContext dbContext)
    : ISQLRepository<TType, TId>
    where TType : class
{
    private readonly DbSet<TType> dbSet
        = dbContext.Set<TType>();

    public void Add(
        TType item)
    {
        dbSet.Add(item);
        dbContext.SaveChanges();
    }

    public async Task AddAsync(
        TType item)
    {
        await dbSet.AddAsync(item);
        await dbContext.SaveChangesAsync();
    }

    public void AddRange(
        TType[] items)
    {
        dbSet.AddRange(items);
        dbContext.SaveChanges();
    }

    public async Task AddRangeAsync(
        TType[] items)
    {
        await dbSet.AddRangeAsync(items);
        await dbContext.SaveChangesAsync();
    }

    public bool Delete(
        TType entity)
    {
        dbSet.Remove(entity);
        int changes = dbContext.SaveChanges();

        return changes > 0;
    }

    public async Task<bool> DeleteAsync(
        TType entity)
    {
        dbSet.Remove(entity);
        int changes = await dbContext.SaveChangesAsync();

        return changes > 0;
    }

    public TType? FirstOrDefault(
        Func<TType, bool> predicate)
    {
        TType? entity = dbSet
            .FirstOrDefault(predicate);

        return entity;
    }

    public async Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate)
    {
        TType? entity = await dbSet
             .FirstOrDefaultAsync(predicate);

        return entity;
    }

    public IEnumerable<TType> GetAll()
    {
        return [.. dbSet];
    }

    public async Task<IEnumerable<TType>> GetAllAsync()
    {
        return await dbSet.ToArrayAsync();
    }

    public IQueryable<TType> GetAllAttached()
    {
        return dbSet.AsQueryable();
    }

    public TType? GetById(
        TId id)
    {
        TType? entity = dbSet
            .Find(id);

        return entity;
    }

    public async Task<TType?> GetByIdAsync(
        TId id)
    {
        TType? entity = await dbSet
            .FindAsync(id);

        return entity;
    }

    public bool Update(
        TType item)
    {
        try
        {
            dbSet.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(
        TType item)
    {
        try
        {
            dbSet.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}