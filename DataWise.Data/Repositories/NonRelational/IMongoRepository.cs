using System.Linq.Expressions;

namespace DataWise.Data.Repositories.NonRelational;

/// <summary>
/// Defines the methods for interacting with a MongoDB-based repository.
/// Provides CRUD operations for documents stored in a MongoDB collection.
/// </summary>
/// <typeparam name="TType">The type of the entity being managed by the repository.</typeparam>
/// <typeparam name="TId">The type of the identifier for the entity.</typeparam>
public interface IMongoRepository<TType, TId>
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity with the specified identifier, or <c>null</c> if not found.</returns>
    TType? GetById(
        TId id);

    /// <summary>
    /// Asynchronously gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task representing the asynchronous operation, with the entity with the specified identifier, or <c>null</c> if not found.</returns>
    Task<TType?> GetByIdAsync(
        TId id);

    /// <summary>
    /// Gets the first entity that matches the provided predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity.</param>
    /// <returns>The first matching entity, or <c>null</c> if no match is found.</returns>
    TType? FirstOrDefault(
        Func<TType, bool> predicate);

    /// <summary>
    /// Asynchronously gets the first entity that matches the provided predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity.</param>
    /// <returns>A task representing the asynchronous operation, with the first matching entity, or <c>null</c> if no match is found.</returns>
    Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate);

    /// <summary>
    /// Gets all entities in the repository.
    /// </summary>
    /// <returns>A collection of all entities in the repository.</returns>
    IEnumerable<TType> GetAll();

    /// <summary>
    /// Asynchronously gets all entities in the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with a collection of all entities in the repository.</returns>
    Task<IEnumerable<TType>> GetAllAsync();

    /// <summary>
    /// Gets all entities attached to the repository.
    /// </summary>
    /// <returns>An IQueryable that can be used to query the entities.</returns>
    IQueryable<TType> GetAllAttached();

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="item">The entity to add.</param>
    void Add(
        TType item);

    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="item">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(
        TType item);

    /// <summary>
    /// Adds multiple entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    void AddRange(
        TType[] items);

    /// <summary>
    /// Asynchronously adds multiple entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddRangeAsync(
        TType[] items);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns><c>true</c> if the entity was successfully deleted, otherwise <c>false</c>.</returns>
    bool Delete(
        TType entity);

    /// <summary>
    /// Asynchronously deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task representing the asynchronous operation, with a result indicating success or failure.</returns>
    Task<bool> DeleteAsync(
        TType entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns><c>true</c> if the entity was successfully updated, otherwise <c>false</c>.</returns>
    bool Update(
        TType item);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation, with a result indicating success or failure.</returns>
    Task<bool> UpdateAsync(
        TType item);
}
