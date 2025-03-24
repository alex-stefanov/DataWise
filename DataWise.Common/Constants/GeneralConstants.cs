namespace DataWise.Common.Constants;

/// <summary>
/// Provides general constant values used across the application.
/// </summary>
public static class GeneralConstants
{
    /// <summary>
    /// The configuration key for user database settings.
    /// </summary>
    public const string UserDbSettingsName = "UserDbSettings";

    /// <summary>
    /// The configuration key for MongoDB settings.
    /// </summary>
    public const string MongoDbSettingsName = "MongoDbSettings";

    /// <summary>
    /// The name of the collection for storing data structures in MongoDB.
    /// </summary>
    public const string DataStructureCollectionName = "DataStructures";

    /// <summary>
    /// The name of the collection for storing algorithms in MongoDB.
    /// </summary>
    public const string AlgorithmCollectionName = "Algorithms";

    /// <summary>
    /// The policy name used for CORS configuration.
    /// </summary>
    public const string PolicyValue = "AllowAll";

    /// <summary>
    /// The environment variable key for the OpenAI API.
    /// </summary>
    public const string OpenAIApiEnvKey = "OPENAI_API";
}