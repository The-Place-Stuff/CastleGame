using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class AddItemToWorkstationGoal : Goal
{
    public AddItemToWorkstationGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        Villager villager = Character as Villager;

        if (!(Target is Workstation))
        {
            Fail();
            return;
        }

        if (villager.Item.Name == Item.Empty().Name)
        {
            Fail();
            return;
        }

        Workstation workstation = Target as Workstation;

        workstation.AddItem(villager.Item);
        villager.Item = Item.Empty();

        Finish();
    }

    public override void Update()
    {
        
    }
}
