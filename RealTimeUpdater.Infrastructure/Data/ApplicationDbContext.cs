﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeUpdater.Models.Entities;
using System.Reflection;

namespace RealTimeUpdater.Infrastructure.Data
{
	/// <summary>
	/// DbContext for our Application
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, int,
		IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
		IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<AppRole> Roles { get; set; }
		public DbSet<AppUserRole> UserRoles { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<Group> Groups { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}