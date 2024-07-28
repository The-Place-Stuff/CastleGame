using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Bush : Object
{
    public Bush(string name, string path) : base(name, path)
    {
    }

    public override void Load()
    {
        GetComponent<Sprite>().ChangePath(Objects.Path + "bush_berries");
        base.Load();
    }


}
