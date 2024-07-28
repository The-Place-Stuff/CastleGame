using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Objects : Registry
{
    public static new List<Object> List = new List<Object>();
    public static new string Path = "assets/img/objects/";


    public static readonly Object Bush = Register(new Bush("bush", Path + "bush"));

    public static readonly Object Rock = Register(new Rock("rock", Path + "rock"));

    public static readonly Object Campfire = Register(new Campfire("campfire", Path + "campfire"));



    public static Object Register(Object obj)
    {
        List.Add(obj);
        return obj;
    }

    public static void RegisterObjects()
    {
        Debug.WriteLine("Registering Objects for CastleGame!");
    }
}
