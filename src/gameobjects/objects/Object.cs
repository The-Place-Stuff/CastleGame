using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Object : Tile
{
    public ObjectProperties Properties;
    public Object(string name, ObjectProperties objectProperties) : base(name)
    {
        Properties = objectProperties;
    }

    public class ObjectProperties
    {
        public int Durability { get; set; } = 0;

        public ObjectProperties SetDurability(int durability)
        {
            Durability = durability;
            return this;
        }

    }



}

