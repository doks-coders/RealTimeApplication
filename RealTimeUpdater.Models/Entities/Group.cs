using System.ComponentModel.DataAnnotations;

namespace RealTimeUpdater.Models.Entities
{
	/// <summary>
	/// This is the group entity. It contains the hub connections for each user
	/// </summary>
	public class Group
	{
		[Key]
		public string Name { get; set; }

		public ICollection<Connection> Connections { get; set; } = new List<Connection>();
	}
}
