using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class PathFinder
{
    public Dictionary<Vector2, Node> Grid { get; private set; }

    public PathFinder(Dictionary<Vector2, Node> grid)
    {
        Grid = grid;
    }

    public Stack<Node> FindPath(Vector2 start, Vector2 end)
    {
        Node startNode = Grid[start];
        Node endNode = Grid[end];

        PriorityQueue<Node, float> openList = new PriorityQueue<Node, float>();
        List<Node> closedList = new List<Node>();

        List<Node> neighbors = new List<Node>();

        Node currentNode = startNode;

        openList.Enqueue(startNode, startNode.FCost);

        while (openList.Count > 0 && !closedList.Exists(node => node.Position == endNode.Position))
        {
            currentNode = openList.Dequeue();
            closedList.Add(currentNode);

            neighbors = GetNeighbors(currentNode);

            foreach (Node neighbor in neighbors)
            {
                if (closedList.Contains(neighbor)) continue;
                if (!neighbor.Walkable) continue;

                bool alreadyAdded = false;

                foreach (var nodeItem in openList.UnorderedItems)
                {
                    if (nodeItem.Element == neighbor)
                    {
                        alreadyAdded = true;
                        break;
                    }
                }

                if (alreadyAdded) continue;

                neighbor.Parent = currentNode;
                neighbor.DistanceToTarget = Math.Abs(neighbor.Position.X - endNode.Position.X) + Math.Abs(neighbor.Position.Y - endNode.Position.Y);
                neighbor.Cost = neighbor.Weight + neighbor.Parent.Cost;
                openList.Enqueue(neighbor, neighbor.FCost);
            }
        }
        
        if (!closedList.Exists(node => node.Position == endNode.Position)) return null;

        Stack<Node> path = new Stack<Node>();

        Node current = closedList[closedList.IndexOf(currentNode)];

        while (current != null && current != startNode)
        {
            path.Push(current);
            current = current.Parent;
        }

        if (path.Count == 0) return null;

       // Debug.WriteLine("This path's node count is: " + path.Count);

        return path;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Node top = Grid.ContainsKey(new Vector2(node.Position.X, node.Position.Y - 1)) ? Grid[new Vector2(node.Position.X, node.Position.Y - 1)] : null;
        Node bottom = Grid.ContainsKey(new Vector2(node.Position.X, node.Position.Y + 1)) ? Grid[new Vector2(node.Position.X, node.Position.Y + 1)] : null;
        Node left = Grid.ContainsKey(new Vector2(node.Position.X - 1, node.Position.Y)) ? Grid[new Vector2(node.Position.X - 1, node.Position.Y)] : null;
        Node right = Grid.ContainsKey(new Vector2(node.Position.X + 1, node.Position.Y)) ? Grid[new Vector2(node.Position.X + 1, node.Position.Y)] : null;

        if (top != null) neighbors.Add(top);
        if (bottom != null) neighbors.Add(bottom);
        if (left != null) neighbors.Add(left);
        if (right != null) neighbors.Add(right);

        return neighbors;
    }
}
