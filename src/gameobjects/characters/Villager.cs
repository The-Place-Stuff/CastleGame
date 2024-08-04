using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Villager : Character
    {
        public Villager(string name) : base(name)
        {
            Speed = 20;
            Range = 50;

        }

        public override void Update()
        {
            base.Update();

            if (Input.Mouse.LeftClickRelease())
            {
                AddTask(TaskTypes.Go, SceneManager.CurrentScene.GetGameObject<Cursor>());

            }
        }
    }
}
