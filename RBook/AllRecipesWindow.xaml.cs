using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

public partial class AllRecipesWindow : Window
{
    private static readonly HttpClient client = new HttpClient();
    private RecipeContext _context;

    public AllRecipesWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
        LoadRecipes();
    }

    private async void LoadRecipes()
    {
        try
        {
            // Load local recipes
            var localRecipes = _context.Recipes.ToList();

            // Load external recipes
            var externalRecipes = await GetExternalRecipesAsync();

            // Combine both lists
            var allRecipes = localRecipes.Concat(externalRecipes).ToList();

            // Set the combined list as the source for the ListView
            RecipesListView.ItemsSource = allRecipes;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading recipes: {ex.Message}");
        }
    }

    private async Task<List<Recipe>> GetExternalRecipesAsync()
    {
        client.BaseAddress = new Uri("http://localhost:5001/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync("api/recipes");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var recipes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Recipe>>(data);
            return recipes;
        }
        else
        {
            MessageBox.Show("Error retrieving external recipes.");
            return new List<Recipe>();
        }
    }

    private void AddRecipe_Click(object sender, RoutedEventArgs e)
    {
        // Logic to add a new recipe
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        LoadRecipes();
    }
}
