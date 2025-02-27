using DataWise.Common;
using DataWise.Data.DbContexts.NonReleational.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataWise.Data.DbContexts.NonReleational;

public class DataStructuresDbContext
{
    private readonly IMongoDatabase _database;

    public DataStructuresDbContext(
        IOptions<DataStructuresSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<DataStructure> DataStructures =>
        _database.GetCollection<DataStructure>("DataStructures");
}