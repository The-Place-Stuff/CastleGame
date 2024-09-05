using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Drops : Registry
    {
        public static new Dictionary<string, Drop> List = new Dictionary<string, Drop>();

        public static readonly Drop Tree = Register(new Drop(new Drop.Settings().AddDrop(Items.Wood(), 3).SetSource(Objects.Tree())));
        public static readonly Drop Rock = Register(new Drop(new Drop.Settings().AddDrop(Items.Stone()).SetSource(Objects.Rock())));

        public static Drop Register(Drop drop)
        {
            List.Add(drop.DropSettings.Source.Name, drop);
            return drop;
        }


        public static void RegisterRecipes()
        {
            Debug.WriteLine("Registering drops for CastleGame!");
        }
    }
}
