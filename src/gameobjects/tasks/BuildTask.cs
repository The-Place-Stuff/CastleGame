using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class BuildTask : Task
{
    public BuildTask(GameObject obj) : base(obj)
    {

    }

    public BuildTask(Vector2 position) : base(position)
    {

    }

    public override void Start()
    {
        Villager villager = Character as Villager;

        if (!(Target is Blueprint)) return;

        if (villager.Item.Name == Item.Empty().Name) return;

        Blueprint blueprint = Target as Blueprint;


        blueprint.AddItem(villager.Item);
        villager.Item = Item.Empty();



        Finish();
    }
}
