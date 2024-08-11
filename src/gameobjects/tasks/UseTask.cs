﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class UseTask : Task
    {
        public UseTask(GameObject obj) : base(obj)
        {

        }
        public UseTask(Vector2 position) : base(position)
        {

        }
        public override void Start()
        {

            if (Target is Object obj)
            {
                obj.OnUse();
                Character.OnDestinationArrived();
            }

            base.Start();
        }
    }
}
