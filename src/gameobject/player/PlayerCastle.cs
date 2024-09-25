using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class PlayerCastle
{
    public int MaxPopulation { get; private set; } = 0;

    public int Population { get; set; } = 0;

    public List<Villager> Villagers { get; private set; } = new List<Villager>();

    public Landmark Landmark { get; private set; }

    public PlayerCastle(Landmark landmark)
    {
        Landmark = landmark;
    }

    public void AddVillager(Villager villager)
    {
        if (Population >= MaxPopulation) return;

        Villagers.Add(villager);
        Population++;
    }

    public void RemoveVillager(Villager villager)
    {
        Villagers.Remove(villager);
        Population--;
    }

    public List<Object> GetObjectsInRadius()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid objectGrid = map.objectGrid;

        List<Object> objectsInRadius = new List<Object>();

        Vector2 landmarkGridPostion = objectGrid.ConvertWorldCoordinatesToGridCoordinates(Landmark.Position);
        
        float radiusSquared = Landmark.Radius * Landmark.Radius;

        int startX = (int)landmarkGridPostion.X - Landmark.Radius;
        int endX = (int)landmarkGridPostion.X + Landmark.Radius;
        int startY = (int)landmarkGridPostion.Y - Landmark.Radius;
        int endY = (int)landmarkGridPostion.Y + Landmark.Radius;

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);

                Object obj = objectGrid.GetTileFromGridCoordinates(gridPosition) as Object;

                if (obj == null) continue;

                float distanceSquared = Vector2.DistanceSquared(landmarkGridPostion, gridPosition);

                if (distanceSquared <= radiusSquared)
                {
                    objectsInRadius.Add(obj);
                }
            }
        }

        return objectsInRadius;
    }

    public bool IsObjectInRadius(Object obj)
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid objectGrid = map.objectGrid;

        Vector2 landmarkGridPostion = objectGrid.ConvertWorldCoordinatesToGridCoordinates(Landmark.Position);

        float radiusSquared = Landmark.Radius * Landmark.Radius;

        Vector2 gridPosition = objectGrid.ConvertWorldCoordinatesToGridCoordinates(obj.Position);

        float distanceSquared = Vector2.DistanceSquared(landmarkGridPostion, gridPosition);

        return distanceSquared <= radiusSquared;
    }
}
