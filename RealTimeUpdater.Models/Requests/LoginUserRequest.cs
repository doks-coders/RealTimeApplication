namespace RealTimeUpdater.Models.Requests
{
	/// <summary>
	/// This is the request used for logging in a User. 
	/// It will be sent from the FrontEnd
	/// </summary>
	/// <param name="Email"></param>
	/// <param name="Password"></param>
	public record LoginUserRequest(string Email, string Password);
}
