using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class TakeFromStockpileTask : Task
{
    public TakeFromStockpileTask(GameObject bit) : base(bit)
    {

    }
    public TakeFromStockpileTask(Vector2 position) : base(position)
    {

    }
    public override void Start()
    {
        if (!(Target is Stockpile)) return;

        Stockpile stockpile = Target as Stockpile;
        Inventory stockpileInventory = stockpile.GetInventory();

        Villager villager = Character as Villager;

        if (villager.Item.Name == Item.Empty().Name && stockpile.Size > 0)
        {
            villager.Item = stockpileInventory.GetLast();
            stockpile.RemoveItem(stockpileInventory.GetLast());
        }

        Finish();
    }
}
