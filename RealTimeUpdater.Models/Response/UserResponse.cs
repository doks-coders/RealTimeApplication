namespace RealTimeUpdater.Models.Response
{
	/// <summary>
	/// This is the response used for users to front end. 
	/// It will be sent to the FrontEnd
	/// </summary>
	public record UserResponse(string? Email, int Id);
}
