namespace RealTimeUpdater.Models.Requests
{
	/// <summary>
	/// This is the request used for registering a User. 
	/// It will be sent from the FrontEnd
	/// </summary>
	public record RegisterUserRequest(string Email, string Password);

}
