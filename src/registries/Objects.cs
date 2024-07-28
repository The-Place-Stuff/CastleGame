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


    public static Object Bush = Register(new Bush("bush", Path + "bush"));
    public static Object Rock = Register(new Bush("rock", Path + "rock"));



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
