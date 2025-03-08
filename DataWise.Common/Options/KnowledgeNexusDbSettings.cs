namespace DataWise.Common.Options;

/// <summary>
/// Represents the database settings for the Knowledge Nexus section of the application.
/// </summary>
public class KnowledgeNexusDbSettings
{
    /// <summary>
    /// Gets or sets the connection string used to connect to the database.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    public string DatabaseName { get; set; } = null!;
}