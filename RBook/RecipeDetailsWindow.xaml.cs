using CookBook.Models;
using System.Windows;
using System;

public partial class RecipeDetailsWindow : Window
{
    private RecipeContext _context;

    public RecipeDetailsWindow()
    {
        //System.InitializeComponent();
        _context = new RecipeContext();
    }

    private void ShowDetailsButton_Click(object sender, RoutedEventArgs e)
    {
        var recipeId = int.Parse(RecipeIdTextBox.Text);
        var recipe = _context.Recipes.Find(recipeId);
        if (recipe != null)
        {
            RecipeNameTextBlock.Text = recipe.Name;
            RecipeDescriptionTextBlock.Text = recipe.Description;
            RecipeRatingTextBlock.Text = recipe.Rating.ToString();
            // Load image and other details
        }
    }
}
