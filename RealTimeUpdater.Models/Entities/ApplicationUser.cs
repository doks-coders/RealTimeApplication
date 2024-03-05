using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	public class ApplicationUser : IdentityUser<int>
	{
		public ICollection<AppUserRole> AppUserRoles { get; set; }

		public List<Message> InboxMessages { get; set; }
		public List<Message> OutBoxMessages { get; set; }
	}
}
