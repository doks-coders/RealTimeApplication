using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.Models.Entities
{
	public class AppUserRole:IdentityUserRole<int>
	{
		public ApplicationUser AppUser { get; set; }
		public AppRole AppRole { get; set; }	
	}
}
