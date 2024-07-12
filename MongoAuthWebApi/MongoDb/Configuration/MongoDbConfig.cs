namespace MongoAuthWebApi.MongoDb.Configuration;

public class MongoDbConfig
{
    public static readonly string ConfigSection = "MongoDb";

    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;
}
