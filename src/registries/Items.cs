using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Items : Registry
{
    public static new Dictionary<string, Func<Item>> List = new Dictionary<string, Func<Item>>();

    public static new string Path = "assets/img/items/";


    public static readonly Func<Item> Wood = Register(() => new Item("wood"));

    public static readonly Func<Item> Stone = Register( () => new Item("stone"));

    public static readonly Func<Item> Axe = Register(() => new Tool("axe"));

    public static readonly Func<Item> Pickaxe = Register(() => new Tool("pickaxe"));

    public static readonly Func<Item> Shovel = Register(() => new Tool("shovel"));

    public static readonly Func<Item> Sword = Register(() => new Tool("sword"));

    public static readonly Func<Item> Berries = Register(() => new Item("berries"));

    public static readonly Func<Item> IronOre = Register(() => new Item("iron_ore"));

    public static readonly Func<Item> IronBar = Register( () => new Item("iron_bar"));

    public static readonly Func<Item> Wool = Register( () => new Item("wool"));

    public static readonly Func<Item> Meat = Register(() => new Item("meat"));

    public static readonly Func<Item> Steak = Register(() => new Item("steak"));

    public static  Func<Item> Register(Func<Item> item)
    {
        List.Add(item().Name, item);
        return item;
    }

    public static void RegisterItems()
    {
        Debug.WriteLine("Registering items for CastleGame!");
    }

    public static new string GetPath(string name)
    {
        return Path + name;
    }
}
