using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class CastleGameButton : GameObject
    {

        public override void Load()
        {

            NineSliceSprite nineSliceSprite = new NineSliceSprite("assets/img/uis/nineslice");
            Position = new Vector2(70, 70);
            nineSliceSprite.Size = new Vector2(16, 16);
            nineSliceSprite.SetPadding(2);
            AddComponent(nineSliceSprite);

            Button button = new Button(new Vector2(16, 16));
            AddComponent(button);

            button.OnClick += OnClick;

            base.Load();
        }


        public void OnClick(Vector2 targetPosition)
        {
            DebugGui.Log("Clicked at " + targetPosition);
        }
    }
}
