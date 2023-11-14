using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesId",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductImagesId",
                table: "ProductProductImageFile",
                newName: "ProductImagesFilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesFilesId",
                table: "ProductProductImageFile",
                column: "ProductImagesFilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesFilesId",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductImagesFilesId",
                table: "ProductProductImageFile",
                newName: "ProductImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesId",
                table: "ProductProductImageFile",
                column: "ProductImagesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
