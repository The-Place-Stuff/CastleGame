using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Bits : Registry
{
    public static new Dictionary<string, Func<Bit>> List = new Dictionary<string, Func<Bit>>();

    public static new string Path = "bits/";


    public static readonly Func<Bit> Bush = Register(() => new Bush("bush", new Bit.BitProperties()
        .SetDurability(2).AddTags("natural")));

    public static readonly Func<Bit> TallGrass = Register(() => new TallGrass("tallgrass", new Bit.BitProperties()
        .SetDurability(1).SetReplaceable(true).AddTags("natural")));

    public static readonly Func<Bit> Flower = Register(() => new TallGrass("flower", new Bit.BitProperties()
        .SetDurability(1).SetReplaceable(true).AddTags("natural")));

    public static readonly Func<Bit> Rock = Register( () => new Rock("rock", new Bit.BitProperties()
        .SetDurability(4).SetMineable(Items.Pickaxe()).AddTags("natural")));

    public static readonly Func<Bit> Campfire = Register(() => new Campfire("campfire", 10, new Bit.BitProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Bit> Furnace = Register(() => new Furnace("furnace", new Bit.BitProperties()
        .SetDurability(6).SetMineable(Items.Pickaxe())));

    public static readonly Func<Bit> Tree = Register(() => new Tree("tree", new Bit.BitProperties()
        .SetDurability(3).SetMineable(Items.Axe()).AddTags("natural")));

    public static readonly Func<Bit> Stockpile = Register( () => new Stockpile("stockpile", new Bit.BitProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Bit> Blueprint = Register(() => new Blueprint("blueprint"));

    public static readonly Func<Bit> Workbench = Register(() => new Workbench("workbench", new Bit.BitProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Bit> Wall = Register(() => new Wall("wall", new Bit.BitProperties()
        .SetDurability(4).SetMineable(Items.Axe())));

    public static readonly Func<Bit> Tent = Register(() => new Tent("tent", 2, new Bit.BitProperties()
        .SetDurability(5).SetMineable(Items.Axe())));

    public static readonly Func<Bit> IronOre = Register(() => new Rock("iron_ore", new Bit.BitProperties()
    .SetDurability(6).SetMineable(Items.Pickaxe())));

    public static Func<Bit> Register(Func<Bit> bit)
    {

        List.Add(bit().Name, bit);
        return bit;
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
