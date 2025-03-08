using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Provides services for managing algorithms.
/// </summary>
/// <param name="repository">The repository instance to access algorithm data.</param>
public class AlgorithmService(
    IMongoRepository<Algorithm, string> repository)
    : IAlgorithmService
{
    /// <inheritdoc />
    public async Task<Algorithm?> GetAlgorithmByIdAsync(
        string id)
        => await repository
            .GetByIdAsync(id);

    /// <inheritdoc />
    public async Task<Algorithm?> GetAlgorithmByNameAsync(
        string name)
        => await repository
            .FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}