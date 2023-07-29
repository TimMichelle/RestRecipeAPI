using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestRecipeApp.Persistence.Migrations
{
    public partial class AddTableImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Image_ImageId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Image_ImageId",
                table: "Recipes",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId");
        }
    }
}
