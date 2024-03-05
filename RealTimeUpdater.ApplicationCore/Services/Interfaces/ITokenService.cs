using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(ApplicationUser user);
	}
}
