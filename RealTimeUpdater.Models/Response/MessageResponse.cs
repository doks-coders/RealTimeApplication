namespace RealTimeUpdater.Models.Response
{
	/// <summary>
	/// This is the response used for sending a message. 
	/// It will be sent to the FrontEnd
	/// </summary>
	public class MessageResponse
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }
		public int SenderId { get; set; }
		public int RecieverId { get; set; }
		public DateTime DateRead { get; set; }
	}
}
