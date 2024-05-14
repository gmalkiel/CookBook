using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
    public partial class InitialM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Note_Use_RecipeDs_Use_RecipeId",
                table: "Recipe_Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Note",
                table: "Recipe_Note");

            migrationBuilder.RenameTable(
                name: "Recipe_Note",
                newName: "Recipe_NoteDs");

            migrationBuilder.RenameIndex(
                name: "IX_Recipe_Note_Use_RecipeId",
                table: "Recipe_NoteDs",
                newName: "IX_Recipe_NoteDs_Use_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_NoteDs",
                table: "Recipe_NoteDs",
                column: "Recipe_NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_NoteDs_Use_RecipeDs_Use_RecipeId",
                table: "Recipe_NoteDs",
                column: "Use_RecipeId",
                principalTable: "Use_RecipeDs",
                principalColumn: "Use_RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_NoteDs_Use_RecipeDs_Use_RecipeId",
                table: "Recipe_NoteDs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_NoteDs",
                table: "Recipe_NoteDs");

            migrationBuilder.RenameTable(
                name: "Recipe_NoteDs",
                newName: "Recipe_Note");

            migrationBuilder.RenameIndex(
                name: "IX_Recipe_NoteDs_Use_RecipeId",
                table: "Recipe_Note",
                newName: "IX_Recipe_Note_Use_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Note",
                table: "Recipe_Note",
                column: "Recipe_NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Note_Use_RecipeDs_Use_RecipeId",
                table: "Recipe_Note",
                column: "Use_RecipeId",
                principalTable: "Use_RecipeDs",
                principalColumn: "Use_RecipeId");
        }
    }
}
