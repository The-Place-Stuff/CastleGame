using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Fog : Bit
{
    bool halfOpacity = false;

    public Fog(string name) : base(name, new BitProperties())
    {
    }

    public override void Load()
    {
        Sprite sprite = new Sprite("assets/img/tiles/fog");
        AddComponent(sprite);
        base.Load();
    }

    public override void Update()
    {


    }
}
