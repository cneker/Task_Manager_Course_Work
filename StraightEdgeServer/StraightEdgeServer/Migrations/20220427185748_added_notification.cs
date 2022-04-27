using Microsoft.EntityFrameworkCore.Migrations;

namespace StraightEdgeServer.Migrations
{
    public partial class added_notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationEnabled",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationEnabled",
                table: "Tasks");
        }
    }
}
