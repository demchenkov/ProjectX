using Infrastructure.ExternalServices.Interfaces.JwtEncodingKey;
using Infrastructure.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Web.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBearerAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();

            var serviceProvider = services.BuildServiceProvider();
            // Resolve the services from the service provider
            var decodingKey = serviceProvider.GetService<IJwtSigningDecodingKey>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = decodingKey.GetPublicKey(),
                        
                        ValidateIssuer = true,
                        ValidIssuer = appSettings.Issuer,

                        ValidateLifetime = true,
                        ValidateAudience = false
                        //ValidateAudience = true,
                        //ValidAudience = appSettings.Audience
                    };
                });

            return services;
        }

        public static IServiceCollection RegisterAllOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);


            return services;
        }
    }
}