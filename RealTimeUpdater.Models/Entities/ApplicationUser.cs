using Microsoft.AspNetCore.Identity;

namespace RealTimeUpdater.Models.Entities
{
	/// <summary>
	/// This is the User entity for our Application. It is used for storing the content of our 
	/// user in the database
	/// </summary>
	public class ApplicationUser : IdentityUser<int>
	{
		public ICollection<AppUserRole> AppUserRoles { get; set; }

		public List<Message> InboxMessages { get; set; }
		public List<Message> OutBoxMessages { get; set; }
	}
}
