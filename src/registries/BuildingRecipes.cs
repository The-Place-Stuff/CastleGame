using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class BuildingRecipes : Registry
    {
        public static new Dictionary<string, Recipe> List = new Dictionary<string, Recipe>();


        public static readonly Recipe Stockpile = Register(
            new Recipe(new Recipe.Settings().AddIngredient(Items.Wood(), 3).SetType(Objects.Stockpile())));
        public static Recipe Register(Recipe recipe)
        {
            List.Add(recipe.RecipeSettings.Output.Name, recipe);
            return recipe;
        }


        public static void RegisterRecipes()
        {
            Debug.WriteLine("Registering Recipes for CastleGame!");
        }
    }
}
