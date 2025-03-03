using System.Linq.Expressions;

namespace DataWise.Data.Repositories.Releational;

public interface ISQLRepository<TType, TId>
{
    TType? GetById(
        TId id);

    Task<TType?> GetByIdAsync(
        TId id);

    TType? FirstOrDefault(
        Func<TType, bool> predicate);

    Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate);

    IEnumerable<TType> GetAll();

    Task<IEnumerable<TType>> GetAllAsync();

    IQueryable<TType> GetAllAttached();

    void Add(
        TType item);

    Task AddAsync(
        TType item);

    void AddRange(
        TType[] items);

    Task AddRangeAsync(
        TType[] items);

    bool Delete(
        TType entity);

    Task<bool> DeleteAsync(
        TType entity);

    bool Update(
        TType item);

    Task<bool> UpdateAsync(
        TType item);
}