namespace Tululoo.Core.Models
{
    public class TileModel
    {
        public string Name { get; set; } = "";
        public string Background { get; set; } = "";
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Depth { get; set; }
        public string Scene { get; set; } = "";
        public int Image { get; set; }
        public bool Selected { get; set; }
    }
}