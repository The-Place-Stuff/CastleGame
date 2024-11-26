using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Node
{
    public Node Parent {get; set;}
    public Vector2 Position {get; private set;}
    public float DistanceToTarget { get; set; } = -1;
    public float Cost { get; set; } = 1;
    public float Weight {get; set;}
    public float FCost
    {
        get
        {
            if (DistanceToTarget != 1 && Cost != -1)
            {
                return DistanceToTarget + Cost;
            }

            return -1;
        }
    }
    public bool Walkable {get; set;}

    public Node(Vector2 position, bool walkable, float weight = 1)
    {
        Position = position;
        Weight = weight;
        Walkable = walkable;
    }
}
