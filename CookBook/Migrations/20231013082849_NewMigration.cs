using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasurementUnit",
                table: "IngredientMeasurementDs",
                newName: "MeasurementIngredientUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasurementIngredientUnit",
                table: "IngredientMeasurementDs",
                newName: "MeasurementUnit");
        }
    }
}
