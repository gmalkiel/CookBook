using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CookBook.Models
{
    public class RecipeContext:DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        { }
        public DbSet<Recipe> RecipeDs { get; set; } = null!;
        public DbSet<Ingredients> IngredientsDs { get; set; } = null!;
        public DbSet<Use_Recipe> Use_RecipeDs { get; set; } = null!;
        public DbSet<IngredientMeasurement> IngredientMeasurementDs { get; set; } = null!;
    }
}
