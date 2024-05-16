using CookBook.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RBook
{
    /// <summary>
    /// Interaction logic for EditRecipeWindow.xaml
    /// </summary>
    public partial class EditRecipeWindow : Window
    {
        private Recipe _recipe;

        public EditRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            _recipe = recipe;
            LoadRecipeDetails();
        }

        private void LoadRecipeDetails()
        {
            titleTextBox.Text = _recipe.Title;
            descriptionTextBox.Text = _recipe.Description;
            ingredientsTextBox.Text = _recipe.Ingredients; 
            instructionsTextBox.Text = _recipe.Instructions; 
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _recipe.Title = titleTextBox.Text;
            _recipe.Description = descriptionTextBox.Text;
            _recipe.RecipeIngredients. = ingredientsTextBox.Text;
            _recipe.Instructions = instructionsTextBox.Text;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(_recipe);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"https://localhost:7047/api/Recipe/{_recipe.RecipeId}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Recipe updated successfully.");
                        this.Close();
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
