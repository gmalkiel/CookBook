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

                //using (var httpClient = new HttpClient())
                //{
                //    using (var reciperesponse = await httpClient.GetAsync($"https://localhost:7047/api/Recipe/searchbykeyword/{"banana"}"))
                //    {
                //        if (response.IsSuccessStatusCode)
                //        {
                //            return Ok(reciperesponse);
                //        }
                //        else
                //        {
                //            //639152
                //            return BadRequest("Failed to retrieve recipe from public repository.");
                //        }
                //    }
                //}

                return Ok(recipe);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            return Ok(response);
        }

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
                    string url1 = "https://api.spoonacular.com/recipes/" + recipe.Id + "/information?apiKey=7f99c6c016c04352aacafbb6da33a8fe";
                    var client1 = new RestClient(url1);
                    var request1 = new RestRequest(new Uri(url1), Method.Get);
                    RestResponse response1 = client.Execute(request1);
                    RecipeWideInfo recipeInfo = JsonConvert.DeserializeObject<RecipeWideInfo>(response1.Content);
                    //RecipeWideInfo recipeInfo = GetWideInfo(recipe.Id);
                    Recipe newRecipe = new Recipe
                    {
                //        var recipe = await _dbContext.RecipeDs
                //.Include(r => r.RecipeIngredients)
                //.ThenInclude(ri => ri.Ingredient)
                //.FirstOrDefaultAsync(r => r.RecipeId == id);
                        RecipeId = recipe.Id,
                        Title = recipe.Title,
                        Description = recipeInfo.Summary,
                        Instructions = recipeInfo.Instructions
                    };
                    List<IngredientMeasurement> ingredientlist = new List<IngredientMeasurement>();
                    foreach (var ing in recipeInfo.ExtendedIngredients)
                    {
                        var newing = new Ingredients
                        {
                            IngredientName = ing.Name
                        };
                        var newingmeas = new IngredientMeasurement
                        {
                            Quantity = ing.Amount,
                            Ingredient = newing,
                            MeasurementIngredientUnit = ing.Unit
                        };
                        ingredientlist.Add(newingmeas);
                    }
                    newRecipe.RecipeIngredients = ingredientlist;
                    newRecipes.Add(newRecipe);
                }
                return Ok(newRecipes);
            }
            catch (JsonSerializationException ex)
            {
                return NotFound();
            }

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
            try
            {
                List<RecipeSim> recipessim = JsonConvert.DeserializeObject<List<RecipeSim>>(response.Content);
                List<Recipe> recipeinfolist = new List<Recipe>();

                foreach (var recipe in recipessim)
                {
                    string url1 = "https://api.spoonacular.com/recipes/" + recipe.Id + "/information?apiKey=7f99c6c016c04352aacafbb6da33a8fe";
                    var client1 = new RestClient(url1);
                    var request1 = new RestRequest(new Uri(url1), Method.Get);
                    RestResponse response1 = client.Execute(request1);
                    RecipeWideInfo recipeInfo = JsonConvert.DeserializeObject<RecipeWideInfo>(response1.Content);
                    Recipe newRecipe = new Recipe
                    {
                        RecipeId = recipe.Id,
                        Title = recipe.Title,
                        Description = recipeInfo.Summary,
                        Instructions = recipeInfo.Instructions
                    };
                    List<IngredientMeasurement> ingredientlist = new List<IngredientMeasurement>();
                    foreach (var ing in recipeInfo.ExtendedIngredients)
                    {
                        var newing = new Ingredients
                        {
                            IngredientName = ing.Name
                        };
                        var newingmeas = new IngredientMeasurement
                        {
                            Quantity = ing.Amount,
                            Ingredient = newing,
                            MeasurementIngredientUnit = ing.Unit
                        };
                        ingredientlist.Add(newingmeas);
                    }
                    newRecipe.RecipeIngredients = ingredientlist;
                    recipeinfolist.Add(newRecipe);
                }
                return Ok(recipeinfolist);
            }
            catch (JsonSerializationException ex)
            {
                return NotFound();
            }

        }

        // Get recipes by keyword
        // GET /api/recipes/RecipeByKeyword
        [HttpGet("RecipeByKeyword/{keyWord}")]
        public async Task<ActionResult<RecipeSearchResponse>> GetRecipeByKeyword(string keyWord)
        {
            string url1 = "https://api.spoonacular.com/recipes/complexSearch?apiKey=7f99c6c016c04352aacafbb6da33a8fe&query=" + keyWord + "&number=8";
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
                
                //RecipeSearchResponse combinedResponse = new RecipeSearchResponse
                //{
                //    Results = recipeRes1.Results.ToList(),
                //    Offset = recipeRes1.Offset,
                //    Number = recipeRes1.Number,
                //    TotalResults = recipeRes1.TotalResults
                //};
                return Ok(recipeRes1);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }


            return Ok(response1);
        }
    }

}

