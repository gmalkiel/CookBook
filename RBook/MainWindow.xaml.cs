using System;
using System.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void ShowAllRecipes_Click(object sender, RoutedEventArgs e)
    {
        var allRecipesWindow = new AllRecipesWindow();
        allRecipesWindow.Show();
    }

    private void AddRecipe_Click(object sender, RoutedEventArgs e)
    {
        var addRecipeWindow = new AddRecipeWindow();
        addRecipeWindow.Show();
    }

    private void EditRecipe_Click(object sender, RoutedEventArgs e)
    {
        var editRecipeWindow = new EditRecipeWindow();
        editRecipeWindow.Show();
    }

    private void RecipeUsage_Click(object sender, RoutedEventArgs e)
    {
        var recipeUsageWindow = new RecipeUsageWindow();
        recipeUsageWindow.Show();
    }

    private void RecipeDetails_Click(object sender, RoutedEventArgs e)
    {
        var recipeDetailsWindow = new RecipeDetailsWindow();
        recipeDetailsWindow.Show();
    }

    private void EditComments_Click(object sender, RoutedEventArgs e)
    {
        var editCommentsWindow = new EditCommentsWindow();
        editCommentsWindow.Show();
    }

    private void SearchByKeyword_Click(object sender, RoutedEventArgs e)
    {
        var searchByKeywordWindow = new SearchByKeywordWindow();
        searchByKeywordWindow.Show();
    }

    private void SearchByIngredient_Click(object sender, RoutedEventArgs e)
    {
        var searchByIngredientWindow = new SearchByIngredientWindow();
        searchByIngredientWindow.Show();
    }

    private void SimilarRecipes_Click(object sender, RoutedEventArgs e)
    {
        var similarRecipesWindow = new SimilarRecipesWindow();
        similarRecipesWindow.Show();
    }
}
