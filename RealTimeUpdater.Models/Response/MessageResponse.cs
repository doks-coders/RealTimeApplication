using RealTimeUpdater.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
