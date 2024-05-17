using CookBook.Models;
using System.Windows;

public partial class AddRecipeWindow : Window
{
    private RecipeContext _context;

    public AddRecipeWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
    {
        var newRecipe = new Recipe
        {
            Name = RecipeNameTextBox.Text,
            Description = RecipeDescriptionTextBox.Text,
            Ingredients = RecipeIngredientsTextBox.Text.Split(',').ToList()
        };
        _context.Recipes.Add(newRecipe);
        _context.SaveChanges();
        this.Close();
    }
}
