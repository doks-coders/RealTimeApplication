using Microsoft.AspNetCore.Identity;
using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Models.Entities;

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
			return services;
		}
	}
}
