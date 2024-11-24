using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class ItemRecipes : Registry
    {
        public static new Dictionary<string, Recipe> List = new Dictionary<string, Recipe>();

        public static readonly Recipe Axe = Register(new Recipe(new Recipe.Settings().AddIngredient(Items.Wood(), 1).AddIngredient(Items.Stone(), 1).SetType(Bits.Workbench()).SetOutput(Items.Axe())));

        public static readonly Recipe IronBar = Register(new Recipe(new Recipe.Settings().AddIngredient(Items.IronOre(), 2).SetType(Bits.Furnace()).SetOutput(Items.IronBar())));

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
