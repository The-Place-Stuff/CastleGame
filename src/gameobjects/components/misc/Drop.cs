using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Drop : Component
{
    public Settings DropSettings { get; set; }
    public Drop(Settings settings) : base(false)
    {

        DropSettings = settings;

    }


    public class Settings
    {
        public Dictionary<Item, float> Drops { get; set; } = new Dictionary<Item, float>();
        public GameObject Source { get; set; } = GameObject.Empty();
        public Settings AddDrop(Item drop)
        {
            Drops.Add(drop, 1);
            return this;
        }
        public Settings AddDrop(Item drop, float chance)
        {
            Drops.Add(drop, chance);
            return this;
        }
        public Settings SetSource(GameObject source)
        {
            Source = source;
            return this;
        }

    }
}

