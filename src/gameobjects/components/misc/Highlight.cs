using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Highlight : Sprite
{
    public new bool Drawable { get; set; }
    public Highlight(string path) : base(path)
    {
        Drawable = false;
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
        if (!Drawable) return;

        SpriteSheet spritesheet = GameObject.GetComponent<AnimationTree>().CurrentAnimation.SpriteSheet;
        Vector2 size = Size / spritesheet.Size.Y;
        base.Draw(size, spritesheet.Size);
    }





}
