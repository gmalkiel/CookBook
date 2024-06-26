﻿using CookBook.Models;
using System.Windows;

public partial class EditRecipeWindow : Window
{
    private RecipeContext _context;

    public EditRecipeWindow()
    {
        InitializeComponent();
        _context = new RecipeContext();
    }

    private void EditRecipeButton_Click(object sender, RoutedEventArgs e)
    {
        var recipeId = int.Parse(RecipeIdTextBox.Text);
        var recipe = _context.Recipes.Find(recipeId);
        if (recipe != null)
        {
            recipe.Name = RecipeNameTextBox.Text;
            recipe.Description = RecipeDescriptionTextBox.Text;
            recipe.Ingredients = RecipeIngredientsTextBox.Text.Split(',').ToList();
            _context.SaveChanges();
        }
        this.Close();
    }
}
