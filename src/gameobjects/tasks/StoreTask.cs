using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class StoreTask : Task
{
    public StoreTask(GameObject obj) : base(obj)
    {

    }

    public StoreTask(Vector2 position) : base(position)
    {

    }

    public override void Start()
    {
        Villager villager = Character as Villager;

        if (!(Target is Stockpile)) return;

        if (villager.CurrentItem.Name == Item.Empty().Name) return;

        Stockpile stockpile = Target as Stockpile;

        if (villager.CurrentItem.Name == stockpile.CurrentType || stockpile.CurrentType == Item.Empty().Name)
        {
            stockpile.AddItem(villager.CurrentItem);
            villager.CurrentItem = Item.Empty();
        }

        villager.OnDestinationArrived();
    }
}
