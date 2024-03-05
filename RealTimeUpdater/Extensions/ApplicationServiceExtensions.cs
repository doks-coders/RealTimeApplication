using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.ApplicationCore.Services.Services;
using RealTimeUpdater.Infrastructure.Data;

namespace RealTimeUpdater.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
			services.AddScoped<ITokenService, TokenService>();
			return services;
		}
	}
}
