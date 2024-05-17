using CookBook.Models;
using System.Windows;

public partial class SearchByIngredientWindow : Window
{
    private RecipeContext _context;

    public SearchByIngredientWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        var ingredient = IngredientTextBox.Text;
        var recipes = _context.Recipes.Where(r => r.Ingredients.Any(i => i.Contains(ingredient))).ToList();
        IngredientSearchResultsListView.ItemsSource = recipes;
    }
}
