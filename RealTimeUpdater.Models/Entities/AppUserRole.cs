using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	/// <summary>
	/// This is the AppUserRole entity for our Application. It contains the key values for 
	/// our ApplicationUser and AppRole
	/// </summary>
	public class AppUserRole : IdentityUserRole<int>
	{
		public ApplicationUser AppUser { get; set; }
		public AppRole AppRole { get; set; }
	}
}
