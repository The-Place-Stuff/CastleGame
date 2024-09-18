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
    private double timer = 0;

    public ObjectProperties Properties;
    public Object(string name, ObjectProperties objectProperties) : base(name)
    {
        Properties = objectProperties;
    }

    public override void Load()
    {

        Health health = new Health(Properties.Durability);
        AddComponent(health);
        base.Load();
    }
    

    public void Drop(Func<Drop> drop)
    {
        foreach (KeyValuePair<Item, DropProperties> itemEntry in drop().DropSettings.Drops)
        {

            for (int i = 0; i < itemEntry.Value.Count; i++)
            {

                int radius = 0;
                Random random = new Random();
                Vector2 position = new Vector2(random.Next(-radius, radius + 1), random.Next(-radius, radius + 1));
                Item item = Items.Get(itemEntry.Key.Name)();
                float chance = itemEntry.Value.Chance;

                item.Position = Position + position;

                SceneManager.CurrentScene.AddGameObject(item);

            }
        }
    }

    public virtual void Hit(float damage)
    {
        Health health = GetComponent<Health>();
        health.Decrement(damage);

        if (health.IsEmpty()) Destroy();
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

    public override void Update()
    {
        timer += SerpentGame.GameTime.ElapsedGameTime.TotalSeconds;
        if(timer >= 1)
        {
            TimedUpdate();

            Random random = new Random();
            if(random.Next(1, 11) == 1)
            {
                RandomUpdate();
            }

            timer = 0;
        }
        base.Update();
    }

    public virtual void TimedUpdate()
    {

    }

    public virtual void RandomUpdate()
    {

    }


    public class ObjectProperties
    {
        public int Durability { get; set; } = 0;

        public Tool Mineable { get; set; } = Tool.Empty();


        public ObjectProperties SetDurability(int durability)
        {
            Durability = durability;
            return this;
        }

        public ObjectProperties SetMineable(Item item)
        {
            if(item is Tool tool) Mineable = tool;
            return this;
        }

    }



}

