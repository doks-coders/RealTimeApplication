using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.Models.Requests
{
	public record LoginUserRequest(string Email,string Password);
}
