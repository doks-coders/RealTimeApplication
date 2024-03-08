using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeUpdater.Models.Entities;

namespace RealTimeUpdater.Infrastructure.Configurations
{
	/// <summary>
	/// Entity configuration for ApplicationUser
	/// </summary>
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasMany(k => k.AppUserRoles)
				.WithOne(u => u.AppUser)
				.HasForeignKey(u => u.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
