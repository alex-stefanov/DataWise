using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Provides services for handling operations related to data structures.
/// </summary>
/// <param name="repository">The repository instance for data structures.</param>
public class StructureService(
    IMongoRepository<DataStructure, string> repository)
    : IStructureService
{
    /// <inheritdoc />
    public async Task<DataStructure?> GetByIdAsync(
        string id)
        => await repository
        .GetByIdAsync(id);

    /// <inheritdoc />
    public async Task<DataStructure?> GetByNameAsync(
        string name)
        => await repository
        .FirstOrDefaultAsync(ds => ds.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}