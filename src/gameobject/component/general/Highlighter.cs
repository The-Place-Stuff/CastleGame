using Microsoft.Xna.Framework;
using SerpentEngine;

namespace Tira;

public class Highlighter : Sprite
{
    public Highlighter(string path) : base(path)
    {
        Scale += new Vector2(0.2f, 0.2f);

        LayerOffset = -1;

        Color color = Color.LightGoldenrodYellow * 0.5f;
        Color = color;
    }

    public override void Update()
    {
        if (GameObject is Character character)
        {
            AnimationTree animationTree = character.GetComponent<AnimationTree>();

            ChangePath(animationTree.CurrentAnimation.SpriteSheet.CurrentSprite.Path);

            Coordinates = animationTree.CurrentAnimation.SpriteSheet.CurrentSprite.Coordinates;
        }
    }

    public override void Draw()
    {
        if (!Enabled) return;

        SpriteSheet spritesheet = GameObject.GetComponent<AnimationTree>().CurrentAnimation.SpriteSheet;

        Vector2 size = Size / spritesheet.Size.Y;

        base.Draw(size, spritesheet.Size);
    }
}
