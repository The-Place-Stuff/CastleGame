using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class IncubateTask : Task
{
    public IncubateTask(GameObject obj) : base(obj)
    {

    }
    public IncubateTask(Vector2 position) : base(position)
    {

    }
    public override void Start()
    {
        if (!(Target is Coop coop)) return;

        Villager villager = Character as Villager;

        if (villager.Item != Items.Egg()) return;
        coop.AddItem(villager.Item);
        villager.Item = Item.Empty();
        Finish();
    }
}
