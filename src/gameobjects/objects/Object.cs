﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Object : Tile
{
    public Object(string name, string path) : base(new Sprite(path), name)
    {

    }



}
