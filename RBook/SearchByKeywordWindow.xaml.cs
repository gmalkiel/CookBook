using CookBook.Models;
using System.Windows;

public partial class SearchByKeywordWindow : Window
{
    private RecipeContext _context;

    public SearchByKeywordWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        var keyword = KeywordTextBox.Text;
        var recipes = _context.Recipes.Where(r => r.Name.Contains(keyword) || r.Description.Contains(keyword)).ToList();
        KeywordSearchResultsListView.ItemsSource = recipes;
    }
}
