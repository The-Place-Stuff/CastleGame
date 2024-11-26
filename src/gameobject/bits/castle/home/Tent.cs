using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Tent : VillagerHome
{
    public Tent(string name, int maxPopulation, BitProperties bitProperties) : base(name, maxPopulation, bitProperties)
    {
    }

    public override void Load()
    {
        base.Load();

        Sprite sprite = new Sprite(Bits.GetPath(Name, AssetTypes.Image));
        AddComponent(sprite);
    }
}
