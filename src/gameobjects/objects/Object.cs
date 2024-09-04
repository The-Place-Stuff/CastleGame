using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Object : Tile
{
    public ObjectProperties Properties;
    public Object(string name, ObjectProperties objectProperties) : base(name)
    {
        Properties = objectProperties;
    }

    public void Drop(Drop drop)
    {
        foreach (KeyValuePair<Item, float> itemEntry in drop.DropSettings.Drops)
        {
            Item item = itemEntry.Key;
            float chance = itemEntry.Value;

            item.Position = Position;

            SceneManager.CurrentScene.AddGameObject(item);

        }
    }
    
    public void Destroy()
    {
        if (Drops.Get(Name) != null)
        {
            Drop(Drops.Get(Name));
        }

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Vector2 gridPosition = map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position);

        map.objectGrid.RemoveTile(gridPosition);
        
        map.PathFinder.NodeMap.SetWalkable(gridPosition, true);
    }

    public class ObjectProperties
    {
        public int Durability { get; set; } = 0;

        public ObjectProperties SetDurability(int durability)
        {
            Durability = durability;
            return this;
        }

    }



}

