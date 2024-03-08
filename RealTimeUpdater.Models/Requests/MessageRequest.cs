namespace RealTimeUpdater.Models.Requests
{
	/// <summary>
	/// This is the request used for sending a message to a User. 
	/// It will be sent from the FrontEnd
	/// </summary>
	public class MessageRequest
	{
		public string Content { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public int RecieverId { get; set; }
	}
}
