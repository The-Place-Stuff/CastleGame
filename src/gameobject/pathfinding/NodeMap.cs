using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class NodeMap
{
    private Dictionary<Vector2, Node> grid = new Dictionary<Vector2, Node>();

    public NodeMap()
    {
        Refresh();
    }

    public void Refresh()
    {
        grid.Clear();

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        for (int x = (int)-20; x <= 20; x++)
        {
            for (int y = (int)-20; y <= 20; y++)
            {
                Bit bit = map.bitGrid.Bits[new Vector2(x, y)];

                grid.Add(new Vector2(x, y), new Node(new Vector2(x, y), bit == null));
            }
        }
    }

    public Node GetNode(Vector2 position)
    {
        return grid[position];
    }

    public bool HasNode(Vector2 position)
    {
        return grid.ContainsKey(position);
    }

    public void SetWalkable(Vector2 position, bool walkable)
    {
        grid[position].Walkable = walkable;
    }
}
