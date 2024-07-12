namespace MongoAuthWebApi.MongoDb.Configuration;

public class JwtConfig
{
    public static readonly string ConfigSection = "Jwt";

    public string ValidAudience { get; set; } = string.Empty;

    public string ValidIssuer { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public int TokenValidityInMinutes { get; set; } = 30;
}
