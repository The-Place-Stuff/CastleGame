using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class AddItemToWorkbenchTask : Task
{
    public AddItemToWorkbenchTask(GameObject bit) : base(bit)
    {

    }
    public AddItemToWorkbenchTask(Vector2 position) : base(position)
    {

    }
    public override void Start()
    {
        if (!(Target is Workstation workstation)) return;

        Villager villager = Character as Villager;

        workstation.AddItem(villager.Item);
        villager.Item = Item.Empty();
        Finish();
    }
}
