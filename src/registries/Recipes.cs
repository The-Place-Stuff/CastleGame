using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame.src.registries
{
    public class Recipes : Registry
    {
        public static new List<Recipe> List = new List<Recipe>();



        public static Recipe Register(Recipe recipe)
        {
            List.Add(recipe);
            return recipe;
        }
    }
}
