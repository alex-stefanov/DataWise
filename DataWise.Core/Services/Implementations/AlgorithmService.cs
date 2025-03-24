using INTERFACES = DataWise.Core.Services.Interfaces;
using MODELS = DataWise.Data.DbContexts.NonRelational.Models;
using N_RELATIONAL = DataWise.Data.Repositories.NonRelational;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Provides services for managing algorithms.
/// </summary>
/// <param name="repository">The repository instance to access algorithm data.</param>
public class AlgorithmService(
    N_RELATIONAL.IMongoRepository<MODELS.Algorithm, string> repository)
    : INTERFACES.IAlgorithmService
{
    /// <inheritdoc />
    public async Task<MODELS.Algorithm?> GetAlgorithmByIdAsync(
        string id)
        => await repository
            .GetByIdAsync(id);

    /// <inheritdoc />
    public async Task<MODELS.Algorithm?> GetAlgorithmByNameAsync(
        string name)
        => await repository
            .FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}