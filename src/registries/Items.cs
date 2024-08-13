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


    public static Func<Item> Wood = Register("wood",() => new Item("wood"));

    public static Func<Item> Stone = Register("stone", () => new Item("stone"));

    public static Func<Item> Axe = Register("axe", () => new Tool("axe"));

    public static Func<Item> Register(string name, Func<Item> item)
    {
        List.Add(name, item);
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
