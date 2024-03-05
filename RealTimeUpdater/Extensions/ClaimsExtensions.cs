using System.Security.Claims;

namespace RealTimeUpdater.Extensions
{
	public static class ClaimsExtensions
	{
		public static string? GetUserName(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.Name);
		}

		public static int GetUserId(this ClaimsPrincipal user)
		{
			return int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}


