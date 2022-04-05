using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class Interest_registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistration_AspNetUsers_ApplicationUserId",
                table: "InterestRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistration_Items_ItemId",
                table: "InterestRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestRegistration",
                table: "InterestRegistration");

            migrationBuilder.RenameTable(
                name: "InterestRegistration",
                newName: "InterestRegistrations");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistration_ItemId",
                table: "InterestRegistrations",
                newName: "IX_InterestRegistrations_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistration_ApplicationUserId",
                table: "InterestRegistrations",
                newName: "IX_InterestRegistrations_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestRegistrations",
                table: "InterestRegistrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_ApplicationUserId",
                table: "InterestRegistrations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistrations_Items_ItemId",
                table: "InterestRegistrations",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistrations_AspNetUsers_ApplicationUserId",
                table: "InterestRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestRegistrations_Items_ItemId",
                table: "InterestRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestRegistrations",
                table: "InterestRegistrations");

            migrationBuilder.RenameTable(
                name: "InterestRegistrations",
                newName: "InterestRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistrations_ItemId",
                table: "InterestRegistration",
                newName: "IX_InterestRegistration_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestRegistrations_ApplicationUserId",
                table: "InterestRegistration",
                newName: "IX_InterestRegistration_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestRegistration",
                table: "InterestRegistration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistration_AspNetUsers_ApplicationUserId",
                table: "InterestRegistration",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRegistration_Items_ItemId",
                table: "InterestRegistration",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
