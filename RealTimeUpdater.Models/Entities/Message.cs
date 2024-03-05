using System.ComponentModel.DataAnnotations;

namespace RealTimeUpdater.Models.Entities
{
	public class Message
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public ApplicationUser Reciever { get; set; }
		public ApplicationUser Sender { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Deleted { get; set; }
		public int SenderId { get; set; }
		public int RecieverId { get; set; }
		public DateTime DateRead { get; set; }
	}
}
