using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Drop
{
    public Settings DropSettings { get; set; }
    public Drop(Settings settings)
    {
        DropSettings = settings;
    }


    public class Settings
    {
        public Dictionary<Item, DropProperties> Drops { get; set; } = new Dictionary<Item, DropProperties>();
        public GameObject Source { get; set; } = GameObject.Empty();
        public Settings AddDrop(Item drop)
        {
            Drops.Add(drop, new DropProperties(1,1));
            return this;
        }
        public Settings AddDrop(Item drop, int count)
        {

            Drops.Add(drop, new DropProperties(count, 1));
           
            return this;
        }
        public Settings AddDrop(Item drop, int count, float chance)
        {

            Drops.Add(drop, new DropProperties(count, chance));

            return this;
        }
        public Settings AddDrop(Item drop, float chance)
        {
            Drops.Add(drop, new DropProperties(1, chance));
            return this;
        }
        public Settings SetSource(GameObject source)
        {
            Source = source;
            return this;
        }

    }
}

