using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class BitGrid
{
    public int Layer { get; set; } = 0;
    public int TileSize { get; set; } = 16;
    public Dictionary<Vector2, Bit> Bits { get; private set; } = new Dictionary<Vector2, Bit>();

    public BitGrid()
    {
    }

    public Bit AddBit(Vector2 coordinates, Func<Bit> bitFunc)
    {
        Bit bit = bitFunc();
        bit.Position = ConvertGridCoordinatesToWorldCoordinates(coordinates);
        bit.Layer = Layer;

        if (Bits.ContainsKey(coordinates))
        {
            RemoveBit(coordinates);
        }

        Bits.Add(coordinates, bit);

        SceneManager.CurrentScene.AddGameObject(bit);

        return bit;
    }

    public void RemoveBit(Vector2 coordinates)
    {
        if (!Bits.ContainsKey(coordinates)) return;

        SceneManager.CurrentScene.Remove(Bits[coordinates]);
        Bits.Remove(coordinates);
    }

    public Bit GetBit(Vector2 coordinates)
    {
        if (!Bits.ContainsKey(coordinates)) return null;

        return Bits[coordinates];
    }

    public Bit North(Bit bit)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, -1))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, -1));
    }

    public Bit East(Bit bit)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(1, 0))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(1, 0));
    }

    public Bit West(Bit bit)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(-1, 0))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(-1, 0));
    }

    public Bit South(Bit bit)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, -1))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, +1));
    }

    public Bit North(Bit bit, int bits)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, -bits))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, -bits));
    }

    public Bit East(Bit bit, int bits)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(bits, 0))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(bits, 0));
    }

    public Bit West(Bit bit, int bits)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(-bits, 0))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(-bits, 0));
    }

    public Bit South(Bit bit, int bits)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, bits))) return Bit.Empty();

        return GetBit(ConvertWorldCoordinatesToGridCoordinates(bit.Position) + new Vector2(0, bits));
    }


    public Vector2 ConvertWorldCoordinatesToGridCoordinates(Vector2 worldCoordinates)
    {
        return new Vector2((int)(worldCoordinates.X / TileSize), (int)(worldCoordinates.Y / TileSize));
    }

    public Vector2 ConvertGridCoordinatesToWorldCoordinates(Vector2 gridCoordinates)
    {
        return new Vector2(gridCoordinates.X * TileSize, gridCoordinates.Y * TileSize);
    }


}
