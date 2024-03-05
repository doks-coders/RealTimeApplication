namespace RealTimeUpdater.Models.Requests
{
	public class MessageRequest
	{
		public string Content { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public int RecieverId { get; set; }
	}
}
