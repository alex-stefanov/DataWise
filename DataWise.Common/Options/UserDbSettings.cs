namespace DataWise.Common.Options;

/// <summary>
/// Represents the settings required for the User database connection.
/// </summary>
public class UserDbSettings
{
    /// <summary>
    /// Gets or sets the connection string to the User database.
    /// </summary>
    /// <value>The connection string used to connect to the User database.</value>
    public string ConnectionString { get; set; } = null!;
}