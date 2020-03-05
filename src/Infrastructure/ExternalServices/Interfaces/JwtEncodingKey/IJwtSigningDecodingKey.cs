using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ExternalServices.Interfaces.JwtEncodingKey
{
    public interface IJwtSigningDecodingKey
    {
        // Key to verify the signature (public)
        SecurityKey GetPublicKey();
    }
}