using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CastleGame;

public class ConnectableModel
{
    [JsonPropertyName("spritesheet")]
    public string SpriteSheet { get; set;}

    [JsonPropertyName("frame_size")]
    public Vector2 FrameSize { get; set; }

    [JsonPropertyName("default_offset")]
    public Vector2 DefaultOffset { get; set; }

    [JsonPropertyName("conditions")]
    public List<Condition> Conditions { get; set; }

    public class Condition
    {
        [JsonPropertyName("search_directions")]
        public List<string> SearchDirections { get; set; }

        [JsonPropertyName("offset")]
        public Vector2 Offset { get; set; }
    }
}
