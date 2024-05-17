using CookBook.Models;
using System.Windows;

public partial class RecipeUsageWindow : Window
{
    private RecipeContext _context;

    public RecipeUsageWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void ShowUsageButton_Click(object sender, RoutedEventArgs e)
    {
        var recipeId = int.Parse(RecipeIdTextBox.Text);
        var usages = _context.RecipeUsages.Where(usage => usage.RecipeId == recipeId).ToList();
        UsageListView.ItemsSource = usages;
    }
}
