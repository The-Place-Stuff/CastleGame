using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Objects : Registry
{
    public static new Dictionary<string, Func<Object>> List = new Dictionary<string, Func<Object>>();

    public static new string Path = "assets/img/objects/";


    public static readonly Func<Object> Bush = Register("bush", () => new Bush("bush", Path + "bush"));

    public static readonly Func<Object> Rock = Register("rock", () => new Rock("rock", Path + "rock"));

    public static readonly Func<Object> Campfire = Register("campfire", () => new Campfire("campfire"));



    public static Func<Object> Register(string name, Func<Object> obj)
    {
        List.Add(name, obj);
        return obj;
    }

    public static void RegisterObjects()
    {
        Debug.WriteLine("Registering Objects for CastleGame!");
    }
}
