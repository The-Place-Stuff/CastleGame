using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Health : Component
{
    public float Points { get; set; } = 0;

    public float Size { get; set; } = 0;
    public Health(float size) : base(false)
    {
        Size = size;
    }


    public void Decrement()
    {
        Points--;
    }

    public void Decrement(float size)
    {
        Points -= size;
    }

    public void Increment()
    {
        Points++;
    }
    public void Increment(float size)
    {
        Points += size;
    }

    public bool IsEmpty()
    {
        return Points == 0;
    }
}
