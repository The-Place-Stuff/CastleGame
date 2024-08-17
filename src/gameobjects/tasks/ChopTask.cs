using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class ChopTask : Task
{
    public ChopTask(GameObject obj) : base( obj)
    {

    }
    public ChopTask(Vector2 position) : base(position)
    {

    }
    public override void Start()
    {
        if (!(Target is Tree)) return;

        Tree tree = Target as Tree;

        tree.OnChop();
        Finish();
    }
}
