using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Recipes : Registry
    {
        public static new Dictionary<string, Recipe> List = new Dictionary<string, Recipe>();

        public static readonly Recipe Axe = Register(new Recipe(Items.Wood(), Items.Stone(), Items.Axe()));
        public static Recipe Register(Recipe recipe)
        {
            List.Add(recipe.Output.Name, recipe);
            return recipe;
        }


        public static void RegisterRecipes()
        {
            Debug.WriteLine("Registering Recipes for CastleGame!");
        }
    }
}
