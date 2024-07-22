using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Game : Scene
{
    public Test Test { get; private set; }

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        Test = new Test();

        AddGameObject(Test);
    }

    public override void Begin()
    {
        Camera.Zoom = 5f;
        Camera.SetTarget(Test);

        Debug.WriteLine("Game Scene Begin");
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();
    }
}
