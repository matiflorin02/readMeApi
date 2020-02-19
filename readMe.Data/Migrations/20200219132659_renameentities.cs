using Microsoft.EntityFrameworkCore.Migrations;

namespace readMe.Data.Migrations
{
    public partial class renameentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddedBooks_Books_BookId",
                table: "AddedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AddedBooks_Wishlists_WishlistId",
                table: "AddedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddedBooks",
                table: "AddedBooks");

            migrationBuilder.RenameTable(
                name: "AddedBooks",
                newName: "WishListBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AddedBooks_BookId",
                table: "WishListBooks",
                newName: "IX_WishListBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListBooks",
                table: "WishListBooks",
                columns: new[] { "WishlistId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishListBooks_Books_BookId",
                table: "WishListBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListBooks_Wishlists_WishlistId",
                table: "WishListBooks",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListBooks_Books_BookId",
                table: "WishListBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListBooks_Wishlists_WishlistId",
                table: "WishListBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListBooks",
                table: "WishListBooks");

            migrationBuilder.RenameTable(
                name: "WishListBooks",
                newName: "AddedBookses");

            migrationBuilder.RenameIndex(
                name: "IX_WishListBooks_BookId",
                table: "AddedBookses",
                newName: "IX_AddedBookses_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddedBookses",
                table: "AddedBookses",
                columns: new[] { "WishlistId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AddedBookses_Books_BookId",
                table: "AddedBookses",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddedBookses_Wishlists_WishlistId",
                table: "AddedBookses",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
