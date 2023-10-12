using System.Text.Json.Serialization;

namespace CookBook.Models
{
    public class Ingredients
    {
        public int IngredientsId { get; set; }
        public string? IngredientName { get; set; }
    }
}
