using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class AddItemToBlueprintGoal : Goal
{
    public AddItemToBlueprintGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        Villager villager = Character as Villager;

        if (!(Target is Blueprint))
        {
            Fail();
            return;
        }

        if (villager.Item.Name == Item.Empty().Name)
        {
            Fail();
            return;
        }

        Blueprint blueprint = Target as Blueprint;

        blueprint.AddItem(villager.Item);
        villager.Item = Item.Empty();

        Finish();
    }

    public override void Update()
    {
        
    }
}
