using System.Linq.Expressions;

namespace DataWise.Data.Repositories.Relational;

/// <summary>
/// Represents a repository interface for performing CRUD operations on a SQL-based data source.
/// </summary>
/// <typeparam name="TType">The type of the entity being managed by the repository.</typeparam>
/// <typeparam name="TId">The type of the identifier for the entity.</typeparam>
public interface ISQLRepository<TType, TId>
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    TType? GetById(
        TId id);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
    Task<TType?> GetByIdAsync(
        TId id);

    /// <summary>
    /// Retrieves the first entity that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <returns>The first entity that matches the predicate, or null if no match is found.</returns>
    TType? FirstOrDefault(
        Func<TType, bool> predicate);

    /// <summary>
    /// Asynchronously retrieves the first entity that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the predicate, or null if no match is found.</returns>
    Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate);

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>An enumerable collection of all entities.</returns>
    IEnumerable<TType> GetAll();

    /// <summary>
    /// Asynchronously retrieves all entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of all entities.</returns>
    Task<IEnumerable<TType>> GetAllAsync();

    /// <summary>
    /// Retrieves all entities with the associated data (e.g., related entities) attached.
    /// </summary>
    /// <returns>An IQueryable of all entities with attached data.</returns>
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
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(
        TType item);

    /// <summary>
    /// Adds a range of entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    void AddRange(
        TType[] items);

    /// <summary>
    /// Asynchronously adds a range of entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRangeAsync(
        TType[] items);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>True if the entity was successfully deleted; otherwise, false.</returns>
    bool Delete(
        TType entity);

    /// <summary>
    /// Asynchronously deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(
        TType entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>True if the entity was successfully updated; otherwise, false.</returns>
    bool Update(
        TType item);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was successfully updated; otherwise, false.</returns>
    Task<bool> UpdateAsync(
        TType item);
}