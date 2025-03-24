using MongoDB.Driver;
using Microsoft.Extensions.Options;
using OPTIONS = DataWise.Common.Options;
using CONSTANTS = DataWise.Common.Constants;
using MODELS = DataWise.Data.DbContexts.NonRelational.Models;

namespace DataWise.Data.DbContexts.NonRelational;

/// <summary>
/// Represents the context for the Knowledge Nexus MongoDB database.
/// Provides access to the collections for data structures and algorithms.
/// </summary>
public class KnowledgeNexusDbContext
{
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="KnowledgeNexusDbContext"/> class.
    /// </summary>
    /// <param name="settings">The settings used to configure the MongoDB connection.</param>
    public KnowledgeNexusDbContext(
        IOptions<OPTIONS.KnowledgeNexusDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    /// <summary>
    /// Gets the collection of data structures from the MongoDB database.
    /// </summary>
    /// <value>The MongoDB collection containing the data structures.</value>
    public IMongoCollection<MODELS.DataStructure> DataStructures =>
        _database.GetCollection<MODELS.DataStructure>(CONSTANTS.GeneralConstants.DataStructureCollectionName);

    /// <summary>
    /// Gets the collection of algorithms from the MongoDB database.
    /// </summary>
    /// <value>The MongoDB collection containing the algorithms.</value>
    public IMongoCollection<MODELS.Algorithm> Algorithms =>
        _database.GetCollection<MODELS.Algorithm>(CONSTANTS.GeneralConstants.AlgorithmCollectionName);
}