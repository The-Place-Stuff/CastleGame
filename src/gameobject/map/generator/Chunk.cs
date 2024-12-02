using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Chunk
{
    public int X { get; set; }
    public int Y { get; set; }

    public Dictionary<Vector2, Bit> Bits { get; private set; } = new Dictionary<Vector2, Bit>();

    public Chunk(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Update()
    {
        foreach (Bit bit in Bits.Values)
        {
            bit.Update();
        }
    }

    public void Draw()
    {
        foreach (Bit bit in Bits.Values)
        {
            bit.Draw();
        }
    }

    public Bit AddBit(Vector2 coordinates, Func<Bit> bitFunc)
    {
        Bit bit = bitFunc();
        bit.Position = BitGrid.ConvertGridCoordinatesToWorldCoordinates(coordinates);
        bit.Layer = 1;

        if (Bits.ContainsKey(coordinates))
        {
            RemoveBit(coordinates);
        }

        bit.Load();

        Bits.Add(coordinates, bit);

        return bit;
    }

    public void RemoveBit(Vector2 coordinates)
    {
        if (!Bits.ContainsKey(coordinates)) return;

        Bits.Remove(coordinates);
    }

    public Bit GetBit(Vector2 coordinates)
    {
        if (!Bits.ContainsKey(coordinates)) return null;

        return Bits[coordinates];
    }
}
