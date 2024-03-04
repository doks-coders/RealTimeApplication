using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.Models.Response
{
	public record SignedUpUserResponse(string Email, string Token);

}
