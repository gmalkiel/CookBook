using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

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
            return await _dbContext.RecipeDs.ToListAsync();
        }

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            if (_dbContext.RecipeDs == null)
            {
                return NotFound();
            }
            var recipe = await _dbContext.RecipeDs.FindAsync(id);

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

        //public async Task<string> SearchRecipes(string keyword)
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/complexSearch?query=pasta&cuisine=italian&excludeCuisine=greek&diet=vegetarian&intolerances=gluten&equipment=pan&includeIngredients=tomato%2Ccheese&excludeIngredients=eggs&type=main%20course&instructionsRequired=true&fillIngredients=false&addRecipeInformation=false&titleMatch=Crock%20Pot&maxReadyTime=20&ignorePantry=true&sort=calories&sortDirection=asc&minCarbs=10&maxCarbs=100&minProtein=10&maxProtein=100&minCalories=50&maxCalories=800&minFat=10&maxFat=100&minAlcohol=0&maxAlcohol=100&minCaffeine=0&maxCaffeine=100&minCopper=0&maxCopper=100&minCalcium=0&maxCalcium=100&minCholine=0&maxCholine=100&minCholesterol=0&maxCholesterol=100&minFluoride=0&maxFluoride=100&minSaturatedFat=0&maxSaturatedFat=100&minVitaminA=0&maxVitaminA=100&minVitaminC=0&maxVitaminC=100&minVitaminD=0&maxVitaminD=100&minVitaminE=0&maxVitaminE=100&minVitaminK=0&maxVitaminK=100&minVitaminB1=0&maxVitaminB1=100&minVitaminB2=0&maxVitaminB2=100&minVitaminB5=0&maxVitaminB5=100&minVitaminB3=0&maxVitaminB3=100&minVitaminB6=0&maxVitaminB6=100&minVitaminB12=0&maxVitaminB12=100&minFiber=0&maxFiber=100&minFolate=0&maxFolate=100&minFolicAcid=0&maxFolicAcid=100&minIodine=0&maxIodine=100&minIron=0&maxIron=100&minMagnesium=0&maxMagnesium=100&minManganese=0&maxManganese=100&minPhosphorus=0&maxPhosphorus=100&minPotassium=0&maxPotassium=100&minSelenium=0&maxSelenium=100&minSodium=0&maxSodium=100&minSugar=0&maxSugar=100&minZinc=0&maxZinc=100&offset=0&number=10&limitLicense=false&ranking=2"),
        //        Headers =
        //        {
        //            { "X-RapidAPI-Key", "de4ebd0dd2mshce82478f1f9fc9fp1f1bfcjsnb79dc254f7ab" },
        //            { "X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
        //        },
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(body);
        //        return body;
        //    }
        //}

        // GET /api/recipes/searchbyingredient
        [HttpGet("searchbyingredient/{IngredientName}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipesByIng(string IngredientName)
        {
            if (_dbContext.IngredientsDs == null)
            {
                return NotFound();
            }

            //var recipesId = await _dbContext.IngredientsDs

            var RecipesContainIng = await _dbContext.RecipeDs
                .Include(r => r.RecipeIngredients) // Include the Ingredients related to the Recipe
                .Where(r => r.RecipeIngredients.Any(i => i.Ingredient.IngredientName.Contains(IngredientName)))
                .ToListAsync();

            if (RecipesContainIng == null)
            {
                return NotFound();
            }

            return Ok(RecipesContainIng);
        }

        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
