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

        if (villager.CurrentItem.Name == Item.Empty().Name) return;

        Blueprint bluprint = Target as Blueprint;

        if (villager.CurrentItem.Name == bluprint.Type || bluprint.Type == Item.Empty().Name)
        {
            bluprint.AddItem(villager.CurrentItem);
            villager.CurrentItem = Item.Empty();
        }

        villager.OnDestinationArrived();
    }
}
