using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class BitRecipes : Registry
    {
        public static new Dictionary<string, Recipe> List = new Dictionary<string, Recipe>();


        public static readonly Recipe Stockpile = Register( 
            new Recipe(new Recipe.Settings().AddIngredient(Items.Wood(), 3).SetType(Bits.Stockpile())));

        public static readonly Recipe Workbench = Register(
            new Recipe(new Recipe.Settings().AddIngredient(Items.Wood(), 1).SetType(Bits.Workbench())));

        public static readonly Recipe Furnace = Register(
          new Recipe(new Recipe.Settings().AddIngredient(Items.Stone(), 4).SetType(Bits.Furnace())));

        public static readonly Recipe Wall = Register(
          new Recipe(new Recipe.Settings().AddIngredient(Items.Wood(), 1).SetType(Bits.Wall())));

        public static Recipe Register(Recipe recipe)
        {
            List.Add(recipe.RecipeSettings.Type, recipe);
            return recipe;
        }


        public static void RegisterRecipes()
        {
            Debug.WriteLine("Registering Recipes for CastleGame!");
        }
    }
}
