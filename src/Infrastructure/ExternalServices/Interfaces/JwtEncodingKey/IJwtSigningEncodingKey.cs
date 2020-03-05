using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ExternalServices.Interfaces.JwtEncodingKey
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetPrivateKey();
    }
}