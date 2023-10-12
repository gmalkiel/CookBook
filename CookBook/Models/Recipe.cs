using System.Text.Json.Serialization;

namespace CookBook.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }

        //[JsonPropertyName("Ingredients")]
        public List<IngredientMeasurement> RecipeIngredients { get; set; }
        
    }

    public class Use_Recipe
    {
        public int Use_RecipeId { get; set; }
        public int RecipeId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UseDate { get; set; }
        public List<Recipe_Note> Notes { get; set; }
        public int Rate { get; set; }
    }

    public class Recipe_Note
    {
        public int Recipe_NoteId { get; set; }
        public string Content { get; set; }
    }
}
