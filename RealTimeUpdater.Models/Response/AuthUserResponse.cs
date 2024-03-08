namespace RealTimeUpdater.Models.Response
{
	/// <summary>
	/// This is the response used for registering a User. 
	/// It will be sent to the FrontEnd
	/// </summary>
	public record AuthUserResponse(string Email, string Token);

}
