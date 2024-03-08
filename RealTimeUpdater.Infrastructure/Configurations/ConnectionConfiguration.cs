using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Configurations
{
	internal class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
	{
		public void Configure(EntityTypeBuilder<Connection> builder)
		{
			builder.HasOne(u => u.Group)
				.WithMany(u => u.Connections)
				.HasForeignKey(u => u.GroupName)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
