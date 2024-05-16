using CookBook.Models;
using Newtonsoft.Json;
using RBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RecipeVM MyVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MyVM = new RecipeVM();
            this.DataContext = MyVM;
        }

        private async void SearchLocal_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchBox.Text;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://localhost:7047/api/Recipe/searchbykeyword?keyword={keyword}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var recipes = JsonConvert.DeserializeObject<List<Recipe>>(responseData); // assuming you have a Recipe class
                        recipeListView.ItemsSource = recipes;
                    }
                    else
                    {
                        MessageBox.Show("Error: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private async void SearchExternal_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchBox_Copy.Text;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://localhost:7047/api/Spoonacular/RecipeByKeyword/{keyword}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var recipes = JsonConvert.DeserializeObject<List<Recipe>>(responseData); // assuming you have a Recipe class
                        externalRecipeListView.ItemsSource = recipes;
                    }
                    else
                    {
                        MessageBox.Show("Error: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addnewrecipe = new addNewRecipe();
            addnewrecipe.ShowDialog();
        }

        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Recipe recipe = button?.Tag as Recipe;
            if (recipe != null)
            {
                var editRecipeWindow = new EditRecipeWindow(recipe);
                editRecipeWindow.ShowDialog();
                // Refresh the recipe list if necessary
            }
        }

        private async void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var recipe = button?.Tag as Recipe;
            if (recipe != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the recipe '{recipe.Title}'?", "Delete Recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var response = await client.DeleteAsync($"https://localhost:7047/api/Recipe/DeleteRecipe/{recipe.RecipeId}");
                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Recipe deleted successfully.");
                                // Refresh the recipe list if necessary
                            }
                            else
                            {
                                MessageBox.Show("Error: " + response.ReasonPhrase);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                    }
                }
            }
        }
    }
}
