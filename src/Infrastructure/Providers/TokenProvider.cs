using Core.Interfaces.Providers;

using Infrastructure.ExternalServices.Interfaces.JwtEncodingKey;
using Infrastructure.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;

        public TokenProvider(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, 
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
                var claimsIdentity = await GetUserIdentityAsync(userName);

                var singingCredentials = new SigningCredentials(
                    _signingEncodingKey.GetPrivateKey(),
                    _signingEncodingKey.SigningAlgorithm);

                var token = new JwtSecurityToken(
                    _appSettings.Issuer,
                    _appSettings.Audience,
                    claimsIdentity.Claims,
                    expires: DateTime.Now.AddMinutes(_appSettings.Lifetime),
                    signingCredentials: singingCredentials);

                return new JwtSecurityTokenHandler().WriteToken(token); ;
            }

            return null;
        }

        private async Task<ClaimsIdentity> GetUserIdentityAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = (await _userManager.GetClaimsAsync(user)).ToList();

                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var claimsIdentity =
                    new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                return claimsIdentity;
            }

            return null;
        }
    }
}