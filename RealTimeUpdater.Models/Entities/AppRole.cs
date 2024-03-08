using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	/// <summary>
	/// This is the AppRole entity for our Application
	/// </summary>
	public class AppRole : IdentityRole<int>
	{
		public ICollection<AppUserRole> AppUserRoles { get; set; }
	}
}
