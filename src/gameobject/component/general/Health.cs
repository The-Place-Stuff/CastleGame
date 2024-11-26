using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Health : Component
{
    public float Points { get; set; } = 0;
    public float Size { get; set; } = 0;
    public Health(float size) : base(false)
    {
        Size = size;
        Points = Size;
    }


    public void Decrement()
    {
        Points--;
    }

    public void Decrement(float amount)
    {
        Points -= amount;
    }

    public void Increment()
    {
        Points++;
    }
    public void Increment(float amount)
    {
        Points += amount;
    }

    public bool IsEmpty()
    {
        return Points <= 0;
    }
}
