using RealTimeUpdater.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.ApplicationCore.Services.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(ApplicationUser user);
	}
}
