﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class CharacterStates : Registry
    {

        public static readonly GameObjectState Wandering = new GameObjectState("wandering");

        public static readonly GameObjectState Using = new GameObjectState("using");

        public static readonly GameObjectState Chopping = new GameObjectState("chopping");

        public static readonly GameObjectState Mining = new GameObjectState("mining");

        public static readonly GameObjectState Fighting = new GameObjectState("fighting");



    }
}
