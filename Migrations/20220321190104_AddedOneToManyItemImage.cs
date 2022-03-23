using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class AddedOneToManyItemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ItemId",
                table: "Image",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Item_ItemId",
                table: "Image",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Item_ItemId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ItemId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Image");
        }
    }
}
