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
    public Bush(string name) : base(name)
    {
        Sprite sprite = new Sprite(Objects.GetPath(name));

        AddComponent(sprite);
    }

    public override void Load()
    {
        base.Load();
    }


}
