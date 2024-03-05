using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	public class AppUserRole : IdentityUserRole<int>
	{
		public ApplicationUser AppUser { get; set; }
		public AppRole AppRole { get; set; }
	}
}
