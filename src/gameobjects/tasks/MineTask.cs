using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class MineTask : Task
    {
        public MineTask(GameObject obj) : base(obj)
        {

        }
        public MineTask(Vector2 position) : base(position)
        {

        }
        public override void Start()
        {

            if (Target is Rock rock)
            {
                rock.OnMine();
                Character.OnDestinationArrived();
            }

            base.Start();
        }
    }
}
