using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeUpdater.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUpdater.Infrastructure.Configurations
{
	public class AppRoleConfigurations : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasMany(k => k.AppUserRoles)
				.WithOne(u => u.AppRole)
				.HasForeignKey(u => u.RoleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction); // Specify ON DELETE NO ACTION

		}
	}
}
