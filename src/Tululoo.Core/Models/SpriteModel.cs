using System.Collections.Generic;

namespace Tululoo.Core.Models
{
    public class SpriteModel
    {
        public string Name { get; set; } = "";
        public string CollisionShape { get; set; } = "";
        public int CollisionRadius { get; set; }
        public int CollisionLeft { get; set; }
        public int CollisionRight { get; set; }
        public int CollisionTop { get; set; }
        public int CollisionBottom { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int UID { get; set; }
        public List<FrameModel> Frames { get; set; } = new();
        public List<string> Keywords { get; set; } = new();
    }
}