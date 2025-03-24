using MODELS = DataWise.Data.DbContexts.NonRelational.Models;

namespace DataWise.Core.Services.Interfaces;

/// <summary>
/// Defines operations for managing algorithms.
/// </summary>
public interface IAlgorithmService
{
    /// <summary>
    /// Retrieves an algorithm by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the algorithm.</param>
    /// <returns>The algorithm if found; otherwise, null.</returns>
    Task<MODELS.Algorithm?> GetAlgorithmByIdAsync(
        string id);

    /// <summary>
    /// Retrieves an algorithm by its name.
    /// </summary>
    /// <param name="name">The name of the algorithm.</param>
    /// <returns>The algorithm if found; otherwise, null.</returns>
    Task<MODELS.Algorithm?> GetAlgorithmByNameAsync(
        string name);
}