using Microsoft.Xna.Framework;
using SerpentEngine;

namespace CastleGame;

public class Highlight : Sprite
{
    public Highlight(string path) : base(path)
    {
        Scale += new Vector2(0.25f, 0.25f);

        LayerOffset = -1;

        Color color = Color.GhostWhite;
        color.A = 100;
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
        SpriteSheet spritesheet = GameObject.GetComponent<AnimationTree>().CurrentAnimation.SpriteSheet;

        Vector2 size = Size / spritesheet.Size.Y;

        base.Draw(size, spritesheet.Size);
    }
}
