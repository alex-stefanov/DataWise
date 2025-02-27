using System.Linq.Expressions;
using MongoDB.Driver;

namespace DataWise.Data.Repositories.NonReleational;

public class MongoRepository<TType, TId>(
    IMongoDatabase database,
    string collectionName)
    : IMongoRepository<TType, TId>
{
    protected readonly IMongoCollection<TType> _collection = database.GetCollection<TType>(collectionName);

    public TType? GetById(
        TId id)
    {
        var filter = Builders<TType>.Filter.Eq("Id", id);
        return _collection.Find(filter).FirstOrDefault();
    }

    public async Task<TType?> GetByIdAsync(
        TId id)
    {
        var filter = Builders<TType>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public TType? FirstOrDefault(
        Func<TType, bool> predicate)
    {
        return _collection.AsQueryable().FirstOrDefault(predicate);
    }

    public async Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync();
    }

    public IEnumerable<TType> GetAll()
    {
        return _collection.Find(_ => true).ToEnumerable();
    }

    public async Task<IEnumerable<TType>> GetAllAsync()
    {
        var result = await _collection.FindAsync(_ => true);
        return result.ToEnumerable();
    }

    public IQueryable<TType> GetAllAttached()
    {
        return _collection.AsQueryable();
    }

    public void Add(
        TType item)
    {
        _collection.InsertOne(item);
    }

    public async Task AddAsync(
        TType item)
    {
        await _collection.InsertOneAsync(item);
    }

    public void AddRange(
        TType[] items)
    {
        _collection.InsertMany(items);
    }

    public async Task AddRangeAsync(
        TType[] items)
    {
        await _collection.InsertManyAsync(items);
    }

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
