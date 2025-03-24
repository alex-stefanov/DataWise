using INTERFACES = DataWise.Core.Services.Interfaces;
using MODELS = DataWise.Data.DbContexts.NonRelational.Models;
using N_RELATIONAL = DataWise.Data.Repositories.NonRelational;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Provides services for handling operations related to data structures.
/// </summary>
/// <param name="repository">The repository instance for data structures.</param>
public class StructureService(
    N_RELATIONAL.IMongoRepository<MODELS.DataStructure, string> repository)
    : INTERFACES.IStructureService
{
    /// <inheritdoc />
    public async Task<MODELS.DataStructure?> GetByIdAsync(
        string id)
        => await repository
            .GetByIdAsync(id);

    /// <inheritdoc />
    public async Task<MODELS.DataStructure?> GetByNameAsync(
        string name)
        => await repository
            .FirstOrDefaultAsync(ds => ds.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}