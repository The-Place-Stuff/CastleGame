using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class PickupItemGoal : Goal
{
    public PickupItemGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        Vector2 targetPosition = Target.Position;

        List<GameObject> targets = SceneManager.CurrentScene.GetGameObjects<Item>().Where(x => x.Position == targetPosition).ToList();

        if (targets.Count <= 0)
        {
            Fail();
            return;
        } else
        {
            Target = targets[0];
        }

        Item item = (Item)Target;

        Villager villager = (Villager)Character;

        Inventory playerInventory = SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>();

        if (item is Tool tool && villager.Tool.Name == Tool.Empty().Name)
        {
            villager.Tool = tool;
            playerInventory.Add(item);
        }
        else
        {
            villager.Item = item;
            playerInventory.Add(item);
        }

        Finish();
    }

    public override void Update()
    {
        
    }
}
