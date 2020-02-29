using AutoMapper;

using Core.Interfaces.Providers;
using Core.Interfaces.Services;
using Infrastructure.ExternalServices.Implementation.JwtEncodingKey;
using Infrastructure.ExternalServices.Interfaces.JwtEncodingKey;
using Infrastructure.Providers;
using Infrastructure.Repositories.Implements;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Web.ExtensionMethods;

using DbContext = Infrastructure.Data.DbContext;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PostgreSQLConnection");

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<DbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DbContext>();

            services.AddControllers().AddNewtonsoftJson();

            services.RegisterAllOptions(Configuration);
            services.AddSingleton<IJwtSigningEncodingKey, SigningSymmetricKey>();
            services.AddSingleton<IJwtSigningDecodingKey, SigningSymmetricKey>();

            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddBearerAuth(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandling();

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
