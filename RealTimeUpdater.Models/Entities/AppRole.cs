using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	public class AppRole : IdentityRole<int>
	{
		public ICollection<AppUserRole> AppUserRoles { get; set; }
	}
}
