namespace Fletchling.Application.Config;

public class JwtConfig
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenValidityInDays { get; set; }
    public string SigningKey { get; set; }
    public string EncryptingKey { get; set; }
}