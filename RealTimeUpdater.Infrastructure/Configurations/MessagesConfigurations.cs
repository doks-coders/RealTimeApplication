using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Configurations
{
	public class MessagesConfigurations : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.HasOne(e => e.Reciever)
				.WithMany(e => e.InboxMessages)
				.HasForeignKey(e => e.RecieverId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.Sender)
				.WithMany(e => e.OutBoxMessages)
				.HasForeignKey(e => e.SenderId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
