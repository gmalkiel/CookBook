using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    namespace Spoonacular
    {
        // Base classes
        //public interface BaseIngredient
        //{
        //    int Id { get; set; }
        //    string Aisle { get; set; }
        //    string Image { get; set; }
        //    string Name { get; set; }
        //    string Original { get; set; }
        //    public string OriginalName { get; set; }
        //    double Amount { get; set; }
        //    string Unit { get; set; }
        //    List<string> Meta { get; set; }
        //}

        // Classes for the SearchRecipesByIng function

        //public class SCIngredient : BaseIngredient
        //{
        //    public string UnitLong { get; set; }
        //    public string UnitShort { get; set; }
        //    public string ExtendedName { get; set; }
        //}

        public class SCIngredient
        {
            public int Id { get; set; }//base
            public double Amount { get; set; }//base
            public string Unit { get; set; }//base
            public string UnitLong { get; set; }
            public string UnitShort { get; set; }
            public string Aisle { get; set; }//base
            public string Name { get; set; }//base
            public string Original { get; set; }//base
            public string OriginalName { get; set; }//base
            public List<string> Meta { get; set; }//base
            public string ExtendedName { get; set; }
            public string Image { get; set; }//base
        }

        public class SCRecipe
        {
            public int Id { get; set; }//base
            public string Title { get; set; }//base
            public string Image { get; set; }//base
            public string ImageType { get; set; }//base
            public int UsedIngredientCount { get; set; }
            public int MissedIngredientCount { get; set; }
            public List<SCIngredient> MissedIngredients { get; set; }
            public List<SCIngredient> UsedIngredients { get; set; }
            public List<string> UnusedIngredients { get; set; }
            public int Likes { get; set; }
        }

        //public class RecipeList
        //{
        //    public List<SCRecipe> Recipes { get; set; }
        //}



        // Classes for the GetRecipeInfoByID function

        public class Measure
        {
            public double Amount { get; set; }
            public string UnitShort { get; set; }
            public string UnitLong { get; set; }
        }

        //public class Ingredient : BaseIngredient
        //{
        //    public string Consistency { get; set; }
        //    public string NameClean { get; set; }
        //    public Measure Measures { get; set; }
        //}

        public class IngredientWideInfo
        {
            public int Id { get; set; }//base
            public string Aisle { get; set; }//base
            public string Image { get; set; }//base
            public string Consistency { get; set; }
            public string Name { get; set; }//base
            public string NameClean { get; set; }
            public string Original { get; set; }//base
            public string OriginalName { get; set; }//base
            public double Amount { get; set; }//base
            public string Unit { get; set; }//base
            public List<string> Meta { get; set; }//base
            public Measure Measures { get; set; }
        }

        public class RecipeWideInfo
        {
            public int Id { get; set; }//base
            public string Title { get; set; }//base
            public int ReadyInMinutes { get; set; }
            public int Servings { get; set; }
            public string SourceUrl { get; set; }
            public string Image { get; set; }//base
            public string ImageType { get; set; }//base
            public string Summary { get; set; }
            public List<string> Cuisines { get; set; }
            public List<string> DishTypes { get; set; }
            public List<string> Diets { get; set; }
            public List<string> Occasions { get; set; }
            public List<IngredientWideInfo> ExtendedIngredients { get; set; }
            public string Instructions { get; set; }
            public List<AnalyzedInstruction> AnalyzedInstructions { get; set; }
            public double SpoonacularScore { get; set; }
            public string SpoonacularSourceUrl { get; set; }
        }

        public class AnalyzedInstruction
        {
            public string Name { get; set; }
            public List<Step> Steps { get; set; }
        }

        public class Step
        {
            public int Number { get; set; }
            public string rStep { get; set; }
            public List<IngredientWideInfo> Ingredients { get; set; }
            public List<Equipment> Equipment { get; set; }
            public Length Length { get; set; }
        }

        public class Equipment
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LocalizedName { get; set; }
            public string Image { get; set; }
        }

        public class Length
        {
            public int Number { get; set; }
            public string Unit { get; set; }
        }


        // Classes for the GetSimilarRecipes function

        public class RecipeSim
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int ReadyInMinutes { get; set; }
            public int Servings { get; set; }
            public string SourceUrl { get; set; }
            public string ImageType { get; set; }
            public List<RecipeWideInfo> ExtendedIngredients { get; set; }
        }


        // Classes for the GetRecipeByKeyword function

        public class RecipeResult
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string ImageType { get; set; }
        }

        public class RecipeSearchResponse
        {
            public List<RecipeResult> Results { get; set; }
            public int Offset { get; set; }
            public int Number { get; set; }
            public int TotalResults { get; set; }
        }

    }
}
