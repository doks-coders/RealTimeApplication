using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.ApplicationCore.Services.Services;
using RealTimeUpdater.Infrastructure.Data;
using RealTimeUpdater.Infrastructure.Repository.Interfaces;
using RealTimeUpdater.Infrastructure.Repository.Repositories;

namespace RealTimeUpdater.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IMessageService, MessageService>();
			return services;
		}
	}
}
