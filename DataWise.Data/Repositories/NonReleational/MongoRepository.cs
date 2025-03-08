using System.Linq.Expressions;
using MongoDB.Driver;

namespace DataWise.Data.Repositories.NonReleational;

/// <summary>
/// Provides MongoDB-specific implementation of the <see cref="IMongoRepository{TType, TId}"/> interface.
/// </summary>
/// <typeparam name="TType">The type of the entity being managed by the repository.</typeparam>
/// <typeparam name="TId">The type of the identifier for the entity.</typeparam>
/// <param name="database">The MongoDB database to interact with.</param>
/// <param name="collectionName">The name of the collection in the database.</param>
public class MongoRepository<TType, TId>(
    IMongoDatabase database,
    string collectionName)
    : IMongoRepository<TType, TId>
{
    protected readonly IMongoCollection<TType> _collection = database
        .GetCollection<TType>(collectionName);

    /// <inheritdoc />
    public TType? GetById(
        TId id)
    {
        var filter = Builders<TType>.Filter.Eq("Id", id);

        return _collection.Find(filter).FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<TType?> GetByIdAsync(
        TId id)
    {
        var filter = Builders<TType>.Filter.Eq("Id", id);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public TType? FirstOrDefault(
        Func<TType, bool> predicate)
        => _collection.AsQueryable().FirstOrDefault(predicate);

    /// <inheritdoc />
    public async Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate)
        => await _collection.Find(predicate).FirstOrDefaultAsync();

    /// <inheritdoc />
    public IEnumerable<TType> GetAll()
        => _collection.Find(_ => true).ToEnumerable();

    /// <inheritdoc />
    public async Task<IEnumerable<TType>> GetAllAsync()
    {
        var result = await _collection.FindAsync(_ => true);

        return result.ToEnumerable();
    }

    /// <inheritdoc />
    public IQueryable<TType> GetAllAttached()
        => _collection.AsQueryable();

    /// <inheritdoc />
    public void Add(
        TType item)
    {
        _collection.InsertOne(item);
    }

    /// <inheritdoc />
    public async Task AddAsync(
        TType item)
    {
        await _collection.InsertOneAsync(item);
    }

    /// <inheritdoc />
    public void AddRange(
        TType[] items)
    {
        _collection.InsertMany(items);
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(
        TType[] items)
    {
        await _collection.InsertManyAsync(items);
    }

    /// <inheritdoc />
    public bool Delete(
        TType entity)
    {
        var id = typeof(TType).GetProperty("Id")?.GetValue(entity);

        if (id is null)
            return false;

        var filter = Builders<TType>.Filter.Eq("Id", id);
        var result = _collection.DeleteOne(filter);

        return result.DeletedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(
        TType entity)
    {
        var id = typeof(TType).GetProperty("Id")?.GetValue(entity);

        if (id is null)
            return false;

        var filter = Builders<TType>.Filter.Eq("Id", id);
        var result = await _collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    /// <inheritdoc />
    public bool Update(
        TType item)
    {
        var id = typeof(TType).GetProperty("Id")?.GetValue(item);

        if (id is null)
            return false;

        var filter = Builders<TType>.Filter.Eq("Id", id);
        var result = _collection.ReplaceOne(filter, item);

        return result.ModifiedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(
        TType item)
    {
        var id = typeof(TType).GetProperty("Id")?.GetValue(item);

        if (id is null)
            return false;

        var filter = Builders<TType>.Filter.Eq("Id", id);
        var result = await _collection.ReplaceOneAsync(filter, item);

        return result.ModifiedCount > 0;
    }
}
