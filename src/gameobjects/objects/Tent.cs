using SerpentEngine;

namespace CastleGame;
public class Tent : Object
{
    public Tent(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {

    }

    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Name));

        AddComponent(sprite);
        base.Load();
    }
}
