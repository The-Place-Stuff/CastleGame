using Microsoft.Xna.Framework;
using SerpentEngine;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public static class BitGrid
{
    public static Bit AddBit(Vector2 coordinates, Func<Bit> bitFunc)
    {
        Chunk chunk = GetChunkFromBitPosition(coordinates);

        Bit bit = chunk.AddBit(coordinates, bitFunc);

        return bit;
    }

    public static void RemoveBit(Vector2 coordinates)
    {
        Chunk chunk = GetChunkFromBitPosition(coordinates);

        chunk.RemoveBit(coordinates);
    }

    public static Bit GetBit(Vector2 coordinates)
    {
        Chunk chunk = GetChunkFromBitPosition(coordinates);

        return chunk.GetBit(coordinates);
    }

    public static List<Bit> GetBits<T>() where T : Bit
    {
        List<Bit> bits = new List<Bit>();

        List<Chunk> chunks = SceneManager.CurrentScene.GetGameObject<Map>().chunks.Values.ToList();

        chunks.ForEach(chunk =>
        {
            bits.AddRange(chunk.GetBits<T>());
        });

        return bits;
    }

    private static Chunk GetChunkFromBitPosition(Vector2 bitPosition)
    {
        int chunkX = (int)MathF.Floor(bitPosition.X / 16);
        int chunkY = (int)MathF.Floor(bitPosition.Y / 16);

        Vector2 position = new Vector2(chunkX, chunkY);

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        if (!map.chunks.ContainsKey(position)) return null;

        return map.chunks[position];
    }

    /*
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
    */

    public static Vector2 ConvertWorldCoordinatesToGridCoordinates(Vector2 worldCoordinates)
    {
        return new Vector2((int)(worldCoordinates.X / 16), (int)(worldCoordinates.Y / 16));
    }

    public static  Vector2 ConvertGridCoordinatesToWorldCoordinates(Vector2 gridCoordinates)
    {
        return new Vector2(gridCoordinates.X * 16, gridCoordinates.Y * 16);
    }


}
