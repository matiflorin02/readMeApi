using Microsoft.EntityFrameworkCore.Migrations;

namespace readMe.Data.Migrations
{
    public partial class connections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId",
                unique: true);
        }
    }
}
