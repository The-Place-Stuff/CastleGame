using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Items : Registry
{
    public static new Dictionary<string, Func<Item>> List = new Dictionary<string, Func<Item>>();

    public static new string Path = "items/";


    public static readonly Func<Item> Wood = Register(() => new Item("wood", new Item.ItemProperties()));

    public static readonly Func<Item> Stone = Register( () => new Item("stone", new Item.ItemProperties()));

    public static readonly Func<Item> Axe = Register(() => new Tool("axe", 3, new Item.ItemProperties()));

    public static readonly Func<Item> Pickaxe = Register(() => new Tool("pickaxe", 3, new Item.ItemProperties()));

    public static readonly Func<Item> Shovel = Register(() => new Tool("shovel", 3, new Item.ItemProperties()));

    public static readonly Func<Item> Sword = Register(() => new Tool("sword", 3, new Item.ItemProperties()));

    public static readonly Func<Item> Berries = Register(() => new Item("berries", new Item.ItemProperties()));

    public static readonly Func<Item> IronOre = Register(() => new Item("iron_ore", new Item.ItemProperties()));

    public static readonly Func<Item> IronBar = Register( () => new Item("iron_bar", new Item.ItemProperties()));

    public static readonly Func<Item> Wool = Register( () => new Item("wool", new Item.ItemProperties()));

    public static readonly Func<Item> Meat = Register(() => new Item("meat", new Item.ItemProperties()));

    public static readonly Func<Item> Steak = Register(() => new Item("steak", new Item.ItemProperties()));

    public static readonly Func<Item> Egg = Register(() => new Item("egg", new Item.ItemProperties()));

    public static  Func<Item> Register(Func<Item> item)
    {
        List.Add(item().Name, item);
        return item;
    }

    public static Func<Item> Get(string name)
    {
        if (!List.ContainsKey(name)) return null;

        return List[name];
    }

    public static void RegisterItems()
    {
        Debug.WriteLine("Registering items for CastleGame!");
    }

    public static string GetPath(string name, string asset)
    {
        return asset + Path + name;
    }
}
