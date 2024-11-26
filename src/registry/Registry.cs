using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Registry
{
    public static List<GameObject> List = new List<GameObject>();
    public static string Path = "assets/img/";

    public static string GetPath(string name, string asset)
    {
        return asset + Path + name;
    }
}
