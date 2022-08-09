using Microsoft.EntityFrameworkCore.Migrations;

namespace TechShed.WebUI.Migrations
{
    public partial class SubscribeEmailSended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailSended",
                table: "Subscribes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSended",
                table: "Subscribes");
        }
    }
}
