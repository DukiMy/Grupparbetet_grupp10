using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class Interest_registration_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_ApplicationUserId",
                table: "InterestRegistrations");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "InterestRegistrations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistrations_ApplicationUserId",
                table: "InterestRegistrations",
                newName: "IX_InterestRegistrations_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_UserId",
                table: "InterestRegistrations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_UserId",
                table: "InterestRegistrations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "InterestRegistrations",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistrations_UserId",
                table: "InterestRegistrations",
                newName: "IX_InterestRegistrations_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_ApplicationUserId",
                table: "InterestRegistrations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
