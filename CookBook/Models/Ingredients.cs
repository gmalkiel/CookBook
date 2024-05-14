using System.Text.Json.Serialization;

namespace CookBook.Models
{
    public class IngredientMeasurement
    {
        public int IngredientMeasurementId { get; set; }
        public double Quantity { get; set; }
        public Ingredients Ingredient { get; set; }
        public string MeasurementIngredientUnit { get; set; }
    }
    public class Ingredients
    {
        public int IngredientsId { get; set; }
        public string? IngredientName { get; set; }
    }
}
