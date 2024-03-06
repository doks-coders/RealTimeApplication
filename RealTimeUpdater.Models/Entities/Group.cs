using System.ComponentModel.DataAnnotations;

namespace RealTimeUpdater.Models.Entities
{
	public class Group
	{
		[Key]
		public string Name { get; set; }

		public ICollection<Connection> Connections { get; set; } = new List<Connection>();
	}
}
