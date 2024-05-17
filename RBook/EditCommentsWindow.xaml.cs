using CookBook.Models;
using System.Windows;

public partial class EditCommentsWindow : Window
{
    private RecipeContext _context;

    public EditCommentsWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void UpdateCommentsButton_Click(object sender, RoutedEventArgs e)
    {
        var recipeId = int.Parse(RecipeIdTextBox.Text);
        var recipe = _context.Recipes.Find(recipeId);
        if (recipe != null)
        {
            recipe.Comments = RecipeCommentsTextBox.Text;
            _context.SaveChanges();
        }
        this.Close();
    }
}
