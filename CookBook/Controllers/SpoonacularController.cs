using Azure.Core;
using CookBook.Models;
using DP.Spoonacular;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.Graph.Constants;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpoonacularController : ControllerBase
    {
        // Gives basic information about the recipes just for the display
        // GET /api/recipes/findByIngredients
        [HttpGet("findByIngredients/{IngredientName}")]
        public async Task<ActionResult<IEnumerable<SCRecipe>>> SearchRecipesByIng(string IngredientName)
        {
            string url = "https://api.spoonacular.com/recipes/findByIngredients?apiKey=7f99c6c016c04352aacafbb6da33a8fe&ingredients=" + IngredientName;
            var client = new RestClient(url);
            var request = new RestRequest(new Uri(url), Method.Get);
            RestResponse response = client.Execute(request);

            if (response == null)
            {
                return NotFound();
            }
            string mytry = response.Content;
            try
            {
                List<SCRecipe> recipes = JsonConvert.DeserializeObject<List<SCRecipe>>(mytry);
                List<Recipe> newRecipes = new List<Recipe>();

                foreach (var recipe in recipes)
                {
                    //string recinfo =  GetRecipeInfoByID(recipe.Id);
                    //Console.WriteLine($"Recipe ID: {recipe.Id}, Title: {recipe.Title}, Likes: {recipe.Likes}");
                    Recipe newRecipe = new Recipe
                    {
                        RecipeId = recipe.Id,
                        Title = recipe.Title,
                        Description = "i dont have yet",
                        Instructions = "i dont have instructions yet"
                    };

                    // Add the new recipe to the new list
                    newRecipes.Add(newRecipe);
                }
                return Ok(newRecipes);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            //RecipeList recipes = JsonConvert.DeserializeObject<RecipeList>(mytry);
            //foreach (DP.Spoonacular.Recipe recipe in recipes) 
            //{
            //    Models.Recipe recipe1 = new Models.Recipe();
            //    recipe1.RecipeId = recipe.Id;
            //    recipe1.Title = recipe.Title;
            //}

            return Ok(response);
        }

        // Get information about a certain recipe by ID
        // /api/recipes/
        [HttpGet("RecipeInformation/{id}")]
        public async Task<ActionResult<RecipeWideInfo>> GetRecipeInfoByID(int id)
        {
            string url = "https://api.spoonacular.com/recipes/" + id + "/information?apiKey=7f99c6c016c04352aacafbb6da33a8fe";
            var client = new RestClient(url);
            var request = new RestRequest(new Uri(url), Method.Get);
            RestResponse response = client.Execute(request);

            if (response == null)
            {
                return NotFound();
            }
            string RecipeInfo = response.Content;
            try
            {
                RecipeWideInfo recipe = JsonConvert.DeserializeObject<RecipeWideInfo>(RecipeInfo);

                // Accessing recipe information
                Console.WriteLine($"Recipe Title: {recipe.Title}");
                Console.WriteLine($"Ready In Minutes: {recipe.ReadyInMinutes}");
                Console.WriteLine($"Servings: {recipe.Servings}");
                Console.WriteLine($"Source URL: {recipe.SourceUrl}");

                // Accessing extended ingredients
                foreach (var ingredient in recipe.ExtendedIngredients)
                {
                    Console.WriteLine($"Ingredient: {ingredient.Name}, Amount: {ingredient.Amount} {ingredient.Unit}");
                }

                // Accessing instructions
                Console.WriteLine("Instructions:");
                Console.WriteLine(recipe.Instructions);

                return Ok(recipe);
                //return recipe;
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }

           
            return Ok(response);
        }

        // Get recipes similar to a certain recipe
        // GET /api/recipes/SimilarRecipes
        [HttpGet("SimilarRecipes/{id}")]
        public async Task<ActionResult<RecipeSim>> GetSimilarRecipes(int id)
        {
            string url = "https://api.spoonacular.com/recipes/" + id + "/similar?apiKey=7f99c6c016c04352aacafbb6da33a8fe&number=5";
            var client = new RestClient(url);
            var request = new RestRequest(new Uri(url), Method.Get);
            RestResponse response = client.Execute(request);

            if (response == null)
            {
                return NotFound();
            }
            string similarRec = response.Content;
            try
            {
                List<RecipeSim> recipessim = JsonConvert.DeserializeObject<List<RecipeSim>>(similarRec);
                List<RecipeWideInfo> recipeinfolist = new List<RecipeWideInfo>();

                foreach (var recipe in recipessim)
                {
                    //var client1 = new RestClient(recipe.SourceUrl);
                    //var request1 = new RestRequest(new Uri(recipe.SourceUrl), Method.Get);
                    //RestResponse response1 = client.Execute(request);
                    //if (response1 == null) { return NotFound(); }
                    //return Ok(response1);
                    //GetRecipeInfoByID(recipe.Id);
                    Console.WriteLine($"Title: {recipe.Title}, Servings: {recipe.Servings}, Source URL: {recipe.SourceUrl}");
                }
                return Ok(recipessim);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }


            return Ok(response);
        }

        // Get recipes by keyword
        // GET /api/recipes/RecipeByKeyword
        [HttpGet("RecipeByKeyword/{keyWord}")]
        public async Task<ActionResult<RecipeSearchResponse>> GetRecipeByKeyword(string keyWord)
        {
            string url1 = "https://api.spoonacular.com/recipes/complexSearch?apiKey=7f99c6c016c04352aacafbb6da33a8fe&query=" + keyWord + "&number=8";
            //string url2 = "https://api.spoonacular.com/recipes/complexSearch?apiKey=7f99c6c016c04352aacafbb6da33a8fe&tags=" + keyWord + "&number=8";

            var client = new RestClient();

            var request1 = new RestRequest(new Uri(url1), Method.Get);
            RestResponse response1 = client.Execute(request1);
            if (response1 == null)
            {
                return NotFound();
            }
            string Recipes1 = response1.Content;
            try
            {
                RecipeSearchResponse recipeRes1 = JsonConvert.DeserializeObject<RecipeSearchResponse>(Recipes1);
                
                RecipeSearchResponse combinedResponse = new RecipeSearchResponse
                {
                    Results = recipeRes1.Results.ToList(),
                    Offset = recipeRes1.Offset,
                    Number = recipeRes1.Number,
                    TotalResults = recipeRes1.TotalResults
                };
                return Ok(combinedResponse);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }


            return Ok(response1);
        }
    }

}

