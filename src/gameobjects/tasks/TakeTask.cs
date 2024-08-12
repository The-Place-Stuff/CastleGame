using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TakeTask : Task
    {
        public TakeTask(GameObject obj) : base(obj)
        {

        }
        public TakeTask(Vector2 position) : base(position)
        {

        }
        public override void Start()
        {
            if (!(Target is Stockpile)) return;

            Stockpile stockpile = Target as Stockpile;
            Inventory stockpileInventory = stockpile.GetInventory();

            Villager villager = Character as Villager;

            if (villager.CurrentItem.Name == Item.Empty().Name && stockpile.Size > 0)
            {
                villager.CurrentItem = stockpileInventory.GetLast();
                stockpile.RemoveItem(stockpileInventory.GetLast());
            }

            villager.OnDestinationArrived();
        }
    }
}
