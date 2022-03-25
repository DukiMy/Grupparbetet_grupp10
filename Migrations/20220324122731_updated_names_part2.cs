using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class updated_names_part2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGallery");

            migrationBuilder.RenameColumn(
                name: "ImgPath",
                table: "Image",
                newName: "URL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Image",
                newName: "ImgPath");

            migrationBuilder.CreateTable(
                name: "ItemGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGallery_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGallery_ItemId",
                table: "ItemGallery",
                column: "ItemId");
        }
    }
}
