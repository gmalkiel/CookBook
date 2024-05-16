using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

//using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DP.Spoonacular;
using static Azure.Core.HttpHeader;
using Azure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeContext _dbContext;

        public RecipeController(RecipeContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<RecipeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            if (_dbContext.RecipeDs == null)
            {
                return NotFound();
            }
            var recipe = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ToListAsync();
            return Ok(recipe);
        }

        //to get a recipe from the local book
        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            
            if (_dbContext.RecipeDs == null)
            {
                return NotFound();
            }
            var recipe = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        // GET /api/recipes/searchbykeyword
        [HttpGet("searchbykeyword")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipes(string keyword)
        {
            if (_dbContext.RecipeDs == null)
            {
                return NotFound();
            }
            var matchingRecipes = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Where(recipe =>
                    recipe.Title.Contains(keyword) ||
                    recipe.Description.Contains(keyword) ||
                    recipe.Instructions.Contains(keyword))
                .ToListAsync();

            if (matchingRecipes == null)
            {
                return NotFound();
            }
            return Ok(matchingRecipes);
        }

        // GET /api/recipes/searchbyingredient
        [HttpGet("searchbyingredient/{IngredientName}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipesByIng(string IngredientName)
        {
            if (_dbContext.IngredientsDs == null)
            {
                return NotFound();
            }
            var RecipesContainIng = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Where(r => r.RecipeIngredients.Any(i => i.Ingredient.IngredientName.Contains(IngredientName)))
                .ToListAsync();

            if (RecipesContainIng == null)
            {
                return NotFound();
            }
            return Ok(RecipesContainIng);
        }


        [HttpPost("SavePublicRecipe/{recipeId}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SavePublicRecipe(int recipeId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7047/api/Spoonacular/RecipeInformation/{recipeId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var recipeContent = await response.Content.ReadAsStringAsync();
                        var recipeWideInfo = JsonConvert.DeserializeObject<RecipeWideInfo>(recipeContent);
                        var recipe = await _dbContext.RecipeDs.FindAsync(recipeId);
                        if (_dbContext.RecipeDs == null || recipe != null)
                        {
                            return NotFound();
                        }
                        var newrecipe = new Recipe
                        {
                            Title = recipeWideInfo.Title,
                            Description = recipeWideInfo.Summary,
                            Instructions = recipeWideInfo.Instructions
                        };
                        List<IngredientMeasurement> ingredientlist = new List<IngredientMeasurement>();
                        foreach (var ing in recipeWideInfo.ExtendedIngredients)
                        {
                            var existingIng = await _dbContext.IngredientsDs
                                .FirstOrDefaultAsync(i => i.IngredientName == ing.Name);
                            if (_dbContext.IngredientsDs == null || existingIng == null)
                            {
                                var newing = new Ingredients
                                {
                                    IngredientName = ing.Name
                                };
                                _dbContext.IngredientsDs.Add(newing);
                                var newingmeas = new IngredientMeasurement
                                {
                                    Quantity = ing.Amount,
                                    Ingredient = newing,
                                    MeasurementIngredientUnit = ing.Unit
                                };
                                ingredientlist.Add(newingmeas);
                                _dbContext.IngredientMeasurementDs.Add(newingmeas);
                            }
                        }
                        newrecipe.RecipeIngredients = ingredientlist;
                        _dbContext.RecipeDs.Add(newrecipe);
                        await _dbContext.SaveChangesAsync();
                        return CreatedAtAction(nameof(GetRecipe), new { id = newrecipe.RecipeId }, newrecipe);
                    }
                    else
                    {
                        return BadRequest("Failed to retrieve recipe from public repository.");
                    }
                }
            }
        }

        [HttpPut("AddSuccessfulRecipe/{id}")]
        public async Task<ActionResult<IEnumerable<UsedRecipeInput>>> AddSuccessfulRecipe(int id, [FromBody] UsedRecipeInput usedRecipeInput)
        {
            try
            {
                var dateContent = "";
                var recipe = await _dbContext.RecipeDs.FindAsync(id);
                if (_dbContext.RecipeDs == null || recipe == null)
                {
                    return NotFound();
                }
                var userecipe = await _dbContext.Use_RecipeDs.Include(r => r.Notes).FirstOrDefaultAsync(ur => ur.RecipeId == id);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://localhost:7047/api/HebCal/GetDateCal"))
                    {
                        dateContent = await response.Content.ReadAsStringAsync();
                    }
                }
                //if there was'nt a usage in this recipe befor
                if (userecipe == null)
                {
                    var successfulRecipe = new Use_Recipe
                    {
                        RecipeId = id,
                        ImageUrl = usedRecipeInput.SRImageUrl,
                        UseDate = dateContent,
                        Rate = usedRecipeInput.SRRate,
                        Notes = usedRecipeInput.SRNotes
                    };
                    foreach (var i in usedRecipeInput.SRNotes)
                    {
                        _dbContext.Recipe_NoteDs.Add(i);
                    }
                    _dbContext.Use_RecipeDs.Add(successfulRecipe);
                    await _dbContext.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetRecipe), new { id = successfulRecipe.RecipeId }, successfulRecipe);
                }
                //if there was a usage in this recipe befor
                else
                {
                    userecipe.Rate = usedRecipeInput.SRRate;
                    userecipe.Notes.AddRange(usedRecipeInput.SRNotes);
                    userecipe.ImageUrl = string.Concat(userecipe.ImageUrl, ",", usedRecipeInput.SRImageUrl);
                    userecipe.UseDate = string.Concat(userecipe.UseDate, "\n", dateContent);
                    _dbContext.Entry(userecipe).State = EntityState.Modified;
                    try
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                return Ok("Successful recipe added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<RecipeController>/5
        [HttpPut("UpdateNote/{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Recipe_Note recipenote)
        {
            if(id != recipenote.Recipe_NoteId)
            {
                return BadRequest();
            }
            _dbContext.Entry(recipenote).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var recipen = await _dbContext.Recipe_NoteDs.Where(r => r.Recipe_NoteId == id).ToListAsync();
                if (recipen != null)
                {
                    throw;
                }
                else { return NotFound(); }
            }
            return NoContent();
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("DeleteRecipe/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if(_dbContext.RecipeDs == null)
            {
                return NotFound();
            }
            var recipe = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }
            var measingredient = recipe.RecipeIngredients;
            foreach (var item in measingredient)
            {
                _dbContext.IngredientsDs.Remove(item.Ingredient);
            }
            _dbContext.IngredientMeasurementDs.RemoveRange(measingredient);
            var usedrecipe = await _dbContext.Use_RecipeDs
                .Include(r => r.Notes)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if(usedrecipe != null)
            {
                var notes = usedrecipe.Notes;

                if (notes != null)
                {
                    _dbContext.Recipe_NoteDs.RemoveRange(notes);
                }
                _dbContext.Use_RecipeDs.Remove(usedrecipe);
            }
            _dbContext.RecipeDs.Remove(recipe);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
