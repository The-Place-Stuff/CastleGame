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
            Villager villager = Character as Villager;

            if (Target is Stockpile stockpile)
            {
                if (villager.CurrentItem.Name == Item.Empty().Name && stockpile.Size > 0)
                {
                    villager.CurrentItem = stockpile.GetInventory().GetLast();
                    stockpile.RemoveItem(stockpile.GetInventory().GetLast());
                }

                villager.OnDestinationArrived();
            }

            base.Start();
        }
    }
}
