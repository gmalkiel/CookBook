namespace CookBook.Models
{
    public enum MeasurementUnit
    {
        Teaspoon,
        Tablespoon,
        Cup,
        Gram,
        Ounce,
        Pound,
        Units,
        Bag,
    }
    public class IngredientMeasurement
    {
        public int IngredientMeasurementId { get; set; }
        public double Quantity { get; set; }
        public Ingredients Ingredient { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}
