using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeUpdater.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddedFewObject : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "GroupName",
				table: "Connections",
				type: "nvarchar(450)",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(450)",
				oldNullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "GroupName",
				table: "Connections",
				type: "nvarchar(450)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)");
		}
	}
}
