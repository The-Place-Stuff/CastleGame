﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Objects : Registry
{
    public static new Dictionary<string, Func<Object>> List = new Dictionary<string, Func<Object>>();

    public static new string Path = "objects/";


    public static readonly Func<Object> Bush = Register(() => new Bush("bush", new Object.ObjectProperties()
        .SetDurability(2).AddTags("natural")));

    public static readonly Func<Object> Rock = Register( () => new Rock("rock", new Object.ObjectProperties()
        .SetDurability(4).SetMineable(Items.Pickaxe()).AddTags("natural")));

    public static readonly Func<Object> Campfire = Register(() => new Campfire("campfire", 10, new Object.ObjectProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Object> Furnace = Register(() => new Furnace("furnace", new Object.ObjectProperties()
        .SetDurability(6).SetMineable(Items.Pickaxe())));

    public static readonly Func<Object> Tree = Register(() => new Tree("tree", new Object.ObjectProperties()
        .SetDurability(3).SetMineable(Items.Axe()).AddTags("natural")));

    public static readonly Func<Object> Stockpile = Register( () => new Stockpile("stockpile", new Object.ObjectProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Object> Blueprint = Register(() => new Blueprint("blueprint"));

    public static readonly Func<Object> Workbench = Register(() => new Workbench("workbench", new Object.ObjectProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Object> Wall = Register(() => new Wall("wall", new Object.ObjectProperties()
        .SetDurability(4).SetMineable(Items.Axe())));

    public static readonly Func<Object> Tent = Register(() => new Tent("tent", 2, new Object.ObjectProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Object> IronOre = Register(() => new Rock("iron_ore", new Object.ObjectProperties()
    .SetDurability(6).SetMineable(Items.Pickaxe())));

    public static Func<Object> Register(Func<Object> obj)
    {

        List.Add(obj().Name, obj);
        return obj;
    }

    public static void RegisterObjects()
    {
        Debug.WriteLine("Registering Objects for CastleGame!");
    }

    public static new string GetPath(string name, string asset)
    {
        return asset + Path + name;
    }


}
