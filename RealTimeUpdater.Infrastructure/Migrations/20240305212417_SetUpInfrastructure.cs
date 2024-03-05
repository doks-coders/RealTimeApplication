using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeUpdater.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class SetUpInfrastructure : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
					Deleted = table.Column<bool>(type: "bit", nullable: false),
					SenderId = table.Column<int>(type: "int", nullable: false),
					RecieverId = table.Column<int>(type: "int", nullable: false),
					DateRead = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_AspNetUsers_RecieverId",
						column: x => x.RecieverId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Messages_AspNetUsers_SenderId",
						column: x => x.SenderId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Messages_RecieverId",
				table: "Messages",
				column: "RecieverId");

			migrationBuilder.CreateIndex(
				name: "IX_Messages_SenderId",
				table: "Messages",
				column: "SenderId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Messages");
		}
	}
}
