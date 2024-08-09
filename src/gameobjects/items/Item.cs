using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Item : GameObject
    {
        public Item(string name)
        {
            Name = name;
        }

        public override void Load()
        {

            Layer = 1;
            Sprite sprite = new Sprite(Items.GetPath(Name));
            AddComponent(sprite);


            base.Load();
        }

        public static new Item Empty()
        {
            return new Item("");
        }
    }
}
