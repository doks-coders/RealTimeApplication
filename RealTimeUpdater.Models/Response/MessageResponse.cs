namespace RealTimeUpdater.Models.Response
{
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
