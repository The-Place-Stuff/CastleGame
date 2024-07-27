using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Objects
    {
        public static string Path = "assets/img/objects/";


        public static Object Bush = new Bush(Path + "bush", "bush");   



        public static void registerObjects()
        {
            Debug.WriteLine("Registering Objects for CastleGame!");
        }
    }
}
