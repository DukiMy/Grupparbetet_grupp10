using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class added_interestregistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "InterestRegistration",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterestRegistration_ApplicationUserId",
                table: "InterestRegistration",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistration_AspNetUsers_ApplicationUserId",
                table: "InterestRegistration",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistration_AspNetUsers_ApplicationUserId",
                table: "InterestRegistration");

            migrationBuilder.DropIndex(
                name: "IX_InterestRegistration_ApplicationUserId",
                table: "InterestRegistration");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "InterestRegistration");
        }
    }
}
