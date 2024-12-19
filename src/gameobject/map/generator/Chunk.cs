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

    private Bit[] bitSnapshot = new Bit[0];

    public Chunk(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Update()
    {
        lock (Bits)
        {
            bitSnapshot = Bits.Values.ToArray();
        }

        foreach (Bit bit in bitSnapshot)
        {
            bit.Update();
        }
    }

    public void Draw()
    {
        foreach (Bit bit in bitSnapshot)
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

    public List<Bit> GetBits<T>() where T : Bit
    {
       return Bits.Values.ToList().Where(bit => bit is T).ToList();
    }
}
