using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Sounds : Registry
{
    public static readonly Sound Click = new Sound(GetPath("click", AssetTypes.Sound), "click");

    public static new string GetPath(string name, string asset)
    {
        return asset + name;
    }



}
