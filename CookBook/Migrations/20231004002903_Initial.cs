using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientsDs",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsDs", x => x.IngredientsId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDs",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDs", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "Use_RecipeDs",
                columns: table => new
                {
                    Use_RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Use_RecipeDs", x => x.Use_RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientMeasurementDs",
                columns: table => new
                {
                    IngredientMeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    MeasurementUnit = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMeasurementDs", x => x.IngredientMeasurementId);
                    table.ForeignKey(
                        name: "FK_IngredientMeasurementDs_IngredientsDs_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "IngredientsDs",
                        principalColumn: "IngredientsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMeasurementDs_RecipeDs_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "RecipeDs",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Note",
                columns: table => new
                {
                    Recipe_NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Use_RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Note", x => x.Recipe_NoteId);
                    table.ForeignKey(
                        name: "FK_Recipe_Note_Use_RecipeDs_Use_RecipeId",
                        column: x => x.Use_RecipeId,
                        principalTable: "Use_RecipeDs",
                        principalColumn: "Use_RecipeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeasurementDs_IngredientsId",
                table: "IngredientMeasurementDs",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeasurementDs_RecipeId",
                table: "IngredientMeasurementDs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Note_Use_RecipeId",
                table: "Recipe_Note",
                column: "Use_RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMeasurementDs");

            migrationBuilder.DropTable(
                name: "Recipe_Note");

            migrationBuilder.DropTable(
                name: "IngredientsDs");

            migrationBuilder.DropTable(
                name: "RecipeDs");

            migrationBuilder.DropTable(
                name: "Use_RecipeDs");
        }
    }
}
