using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Sounds : Registry
{

    public static readonly Sound Hit = new Sound(GetPath("hit", AssetTypes.Sound));

    public static readonly Sound Destory = new Sound(GetPath("destroy", AssetTypes.Sound));

    public static readonly Sound Harvest = new Sound(GetPath("harvest", AssetTypes.Sound));

    public static readonly Sound Place = new Sound(GetPath("place", AssetTypes.Sound));

    public static readonly Sound Build = new Sound(GetPath("build", AssetTypes.Sound));

    public static readonly Sound Click = new Sound(GetPath("click", AssetTypes.Sound));

    public static new string GetPath(string name, string asset)
    {
        return asset + name;
    }



}
