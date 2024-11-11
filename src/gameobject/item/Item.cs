using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Item : GameObject, Interactable
{
    public ItemProperties Properties { get; set; }
    public Item(string name, ItemProperties itemProperties)
    {
        Name = name;
        Properties = itemProperties;
    }

    public override void Load()
    {

        Layer = 2;
        Sprite sprite = new Sprite(Items.GetPath(Name, AssetTypes.Image));
        AddComponent(sprite);


        base.Load();
    }

    public static new Item Empty()
    {
        return new Item("", new ItemProperties());
    }

    public class ItemProperties
    {

    }

    public virtual Villager GetVillager()
    {
        foreach (Villager villager in SceneManager.CurrentScene.GetGameObjects().OfType <Villager>())
        {
            if(villager.Item == this && villager.Item != Item.Empty())
            {
                return villager;
            }
        }
        return null;
    }

    public Goal GetGoalType(Villager villager)
    {
        return new MoveAndPickupItemGoalTree(Position, 0);
    }
}
