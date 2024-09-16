using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CastleGame
{
    public class TextElement : GameObject
    {
        public string Body { get; set; } = "";
        public float Scale = 0.5f;
        public TextElement(string body)
        {
            Body = body;
        }
        public override void Load()
        {
            Text text = new Text("assets/font/peaberry", Body);
            text.Scale = Scale;
            AddComponent(text);
            base.Load();
        }
    }
}
