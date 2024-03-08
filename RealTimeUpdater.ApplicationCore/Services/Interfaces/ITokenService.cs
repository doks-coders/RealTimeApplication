using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface ITokenService
	{
		/// <summary>
		/// This is used creating a token for a user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		Task<string> CreateToken(ApplicationUser user);
	}
}
