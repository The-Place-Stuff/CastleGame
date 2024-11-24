using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Stockpile : Bit
{
    public int Size { get; set; } = 0;

    public string CurrentType { get; set; } = Item.Empty().Name;

    public Stockpile(string name, BitProperties bitProperties) : base(name, bitProperties)
    {

    }

    public override void Load()
    {
        Size = 3;

        Sprite sprite = new Sprite(Bits.GetPath(Name, AssetTypes.Image));
        Inventory inventory = new Inventory(Size);
        AddComponent(inventory);

        AddComponent(sprite);
        base.Load();
    }

    public void AddItem(Item item)
    {
        CurrentType = item.Name;
        GetInventory().Add(item);
        item.Position = new Vector2(Position.X, Position.Y - 4);
        SceneManager.CurrentScene.AddGameObject(item);
    }

    public void RemoveLastItem()
    {
        SceneManager.CurrentScene.Remove(GetInventory().GetLast());
        GetInventory().RemoveLast();
    }

    public void RemoveItem(Item item)
    {
        SceneManager.CurrentScene.Remove(GetInventory().Get(item));
        GetInventory().Remove(item);
    }

    public Inventory GetInventory()
    {
        return GetComponent<Inventory>();
    }
}
