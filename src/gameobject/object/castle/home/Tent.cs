using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Tent : VillagerHome
{
    public Tent(string name, int maxPopulation, ObjectProperties objectProperties) : base(name, maxPopulation, objectProperties)
    {
    }

    public override void Load()
    {
        base.Load();

        Sprite sprite = new Sprite(Objects.GetPath(Name));
        AddComponent(sprite);
    }
}
