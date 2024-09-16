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
    public Bush(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {

    }

    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Name));

        AddComponent(sprite);
        base.Load();
    }


}
