using DataWise.Common.Options;
using DataWise.Data.DbContexts.NonReleational.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataWise.Data.DbContexts.NonReleational;

public class KnowledgeNexusDbContext
{
    private readonly IMongoDatabase _database;

    public KnowledgeNexusDbContext(
        IOptions<KnowledgeNexusDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<DataStructure> DataStructures =>
        _database.GetCollection<DataStructure>("DataStructures");

    public IMongoCollection<Algorithm> Algorithms =>
        _database.GetCollection<Algorithm>("Algorithms");
}