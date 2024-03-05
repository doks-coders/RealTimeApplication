using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	public class ApplicationUser : IdentityUser<int>
	{
		public ICollection<AppUserRole> AppUserRoles { get; set; }
	}
}
