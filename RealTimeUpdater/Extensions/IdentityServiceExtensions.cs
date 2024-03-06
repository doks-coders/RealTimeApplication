using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Models.Entities;
using System.Text;

namespace RealTimeUpdater.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddIdentityCore<ApplicationUser>(e =>
			{
				e.Password.RequireNonAlphanumeric = true;
				e.Password.RequiredLength = 6;
			}).AddRoles<AppRole>()
			.AddRoleManager<RoleManager<AppRole>>()
			.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecurityKey"])),
					ValidateIssuer = false,
					ValidateAudience = false

				};
				options.Events = new JwtBearerEvents()
				{
					OnMessageReceived = (context) =>
					{
						var accessToken = context.Request.Query["access_token"];
						var path = context.HttpContext.Request.Path;

						if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
						{
							context.Token = accessToken;
						}

						return Task.CompletedTask;
					}
				};

			});

			return services;

		}
	}
}
