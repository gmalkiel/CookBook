using CookBook.Models;
using System.Windows;

public partial class SimilarRecipesWindow : Window
{
    private RecipeContext _context;

    public SimilarRecipesWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void ShowSimilarRecipesButton_Click(object sender, RoutedEventArgs e)
    {
        var recipeId = int.Parse(RecipeIdTextBox.Text);
        var recipe = _context.Recipes.Find(recipeId);
        if (recipe != null)
        {
            var similarRecipes = _context.Recipes
                .Where(r => r.Ingredients.Any(i => recipe.Ingredients.Contains(i)) && r.Id != recipeId)
                .ToList();
            SimilarRecipesListView.ItemsSource = similarRecipes;
        }
    }
}
