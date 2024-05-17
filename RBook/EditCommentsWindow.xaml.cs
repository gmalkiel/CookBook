using CookBook.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class EditCommentsWindow : Window
{
    private RecipeContext _context;

    public EditCommentsWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }
    private void RemoveText(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox != null && textBox.Text == textBox.Tag.ToString())
        {
            textBox.Text = "";
            textBox.Foreground = Brushes.Black;
        }
    }

    private void AddText(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = textBox.Tag.ToString();
            textBox.Foreground = Brushes.Gray;
        }
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
