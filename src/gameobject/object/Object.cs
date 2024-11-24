using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Object : GameObject
{
    public ObjectProperties Properties;

    private bool destroyHighlight = false;
    private float destroyHighlightTimer = 0;

    public Object(string name, ObjectProperties objectProperties)
    {
        Name = name;
        Properties = objectProperties;
    }

    public override void Load()
    {
        Health health = new Health(Properties.Durability);
        TransformationManager transformationManager = new TransformationManager(Transformations.ObjectHit);

        AddComponent(transformationManager);
        AddComponent(health);
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
        GetComponent<TransformationManager>().Transform(10);
        Health health = GetComponent<Health>();
        health.Decrement(damage);

        SoundPlayer soundPlayer = GetComponent<SoundPlayer>();

        soundPlayer.PlaySound("hit");

        if (health.IsEmpty()) Destroy();
    }

    public void EnableDestroyHighlight()
    {
        destroyHighlight = true;
    }

    public void DisableDestroyHighlight()
    {
        destroyHighlight = false;
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

        SoundPlayer soundPlayer = GetComponent<SoundPlayer>();

        soundPlayer.PlaySound("destroy");
        
        map.PathFinder.NodeMap.SetWalkable(gridPosition, true);
    }

    public override void Update()
    {
        if (destroyHighlight)
        {
            Sprite sprite = GetComponent<Sprite>();

            Color startColor = Color.White;
            Color endColor = Color.Red;

            float blinkSpeed = 6f;
            destroyHighlightTimer += blinkSpeed * SerpentGame.DeltaTime;

            float destroyPreviewLerpTime = (float)Math.Sin(destroyHighlightTimer) * 0.5f + 0.5f;

            sprite.Color = Color.Lerp(startColor, endColor, destroyPreviewLerpTime);
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
        public List<string> Tags { get; set; } = new List<string>();

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

        public ObjectProperties AddTags(params string[] tags)
        {
            Tags.AddRange(tags);
            return this;
        }

    }
}