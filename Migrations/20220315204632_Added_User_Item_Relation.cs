using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class Added_User_Item_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserItemInterests");

            migrationBuilder.DropColumn(
                name: "BrokerUserId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "BrokerId",
                table: "Item",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterestRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestRegistration_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrokerId",
                table: "Item",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestRegistration_ItemId",
                table: "InterestRegistration",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_BrokerId",
                table: "Item",
                column: "BrokerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_BrokerId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "InterestRegistration");

            migrationBuilder.DropIndex(
                name: "IX_Item_BrokerId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "BrokerUserId",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserItemInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItemInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserItemInterests_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserItemInterests_ItemId",
                table: "UserItemInterests",
                column: "ItemId");
        }
    }
}
