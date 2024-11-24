using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class BitList
{

    public Dictionary<Vector2, Object> Bits { get; set; } = new Dictionary<Vector2, Object>();

        public Object North(Object obj)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, -1))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, -1)];
    }

    public Object East(Object obj)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(1, 0))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(1, 0)];
    }

    public Object West(Object obj)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(-1, 0))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(-1, 0)];
    }

    public Object South(Object obj, int objs)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, objs))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, objs)];
    }

    public Object North(Object obj, int objs)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, -objs))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(0, -objs)];
    }

    public Object East(Object obj, int objs)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(objs, 0))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(objs, 0)];
    }

    public Object West(Object obj, int objs)
    {
        if (!Bits.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(-objs, 0))) return null;

        return Bits[ConvertWorldCoordinatesToGridCoordinates(obj.Position) + new Vector2(-objs, 0)];
    }

    public Vector2 ConvertWorldCoordinatesToGridCoordinates(Vector2 worldCoordinates)
    {
        return new Vector2((int)(worldCoordinates.X / 16), (int)(worldCoordinates.Y / 16));
    }

    public Vector2 ConvertGridCoordinatesToWorldCoordinates(Vector2 gridCoordinates)
    {
        return new Vector2(gridCoordinates.X * 16, gridCoordinates.Y * 16);
    }


}
