using DataWise.Data.DbContexts.NonReleational.Models;

namespace DataWise.Core.Services.Interfaces;

/// <summary>
/// Defines operations for managing data structures.
/// </summary>
public interface IStructureService
{
    /// <summary>
    /// Retrieves a data structure by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data structure.</param>
    /// <returns>The data structure if found; otherwise, null.</returns>
    Task<DataStructure?> GetByIdAsync(
        string id);

    /// <summary>
    /// Retrieves a data structure by its name.
    /// </summary>
    /// <param name="name">The name of the data structure.</param>
    /// <returns>The data structure if found; otherwise, null.</returns>
    Task<DataStructure?> GetByNameAsync(
        string name);
}