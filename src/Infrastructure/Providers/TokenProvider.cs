using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.Interfaces.Providers;
using Infrastructure.ExternalServices.Interfaces.JwtEncodingKey;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;

        public TokenProvider(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IOptions<AppSettings> appSettingsOptions, 
            IJwtSigningEncodingKey signingEncodingKey)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettingsOptions.Value;
            _signingEncodingKey = signingEncodingKey;
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            var result =  await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                var claims = await _userManager.GetClaimsAsync(user);
                var singingCredentials = new SigningCredentials(
                    _signingEncodingKey.GetPrivateKey(),
                    _signingEncodingKey.SigningAlgorithm);

                var token = new JwtSecurityToken(
                    _appSettings.Issuer,
                    _appSettings.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(_appSettings.Lifetime),
                    signingCredentials: singingCredentials);

                return new JwtSecurityTokenHandler().WriteToken(token); ;
            }

            return null;
        }
    }
}