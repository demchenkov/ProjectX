using System.Text;
using Infrastructure.ExternalServices.Interfaces.JwtEncodingKey;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ExternalServices.Implementation.JwtEncodingKey
{
    public class SigningSymmetricKey : IJwtSigningDecodingKey, IJwtSigningEncodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public SigningSymmetricKey(IOptions<AppSettings> appSettingOptions)
        {
            var appSetting = appSettingOptions.Value;
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.Key));
        }

        public SecurityKey GetPrivateKey()
        {
            return _secretKey;
        }

        public SecurityKey GetPublicKey()
        {
            return _secretKey;
        }
    }
}