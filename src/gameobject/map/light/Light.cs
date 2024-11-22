using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Light : Component
{
    public Color Color = Color.White;

    private Rectangle lightRectangle;

    public Light() : base(true)
    {
    }

    public override void Initialize()
    {
        lightRectangle = new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, 16, 16);
    }

    public override void Draw()
    {
        Rectangle bounds = new Rectangle((int)GameObject.Position.X - (int)(16 / 2), (int)GameObject.Position.Y - (int)(16 / 2), 16, 16);

        Texture2D texture2d = SerpentEngine.Draw.Pixel;

        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, bounds, null, Color, 0, Vector2.Zero, SpriteEffects.None, 100 * 0.001f);
    }
}
