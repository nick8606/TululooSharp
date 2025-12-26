using System.Collections.Generic;

namespace Tululoo.Core.Models
{
    public class SceneModel
    {
        public string Name { get; set; } = "";
        public int Width { get; set; } = 320;
        public int Height { get; set; } = 240;
        public int Speed { get; set; } = 60;
        public string Code { get; set; } = "";
        public string Background { get; set; } = "";
        public int UID { get; set; }

        public List<TileModel> Tiles { get; set; } = new();
        public List<LayerModel> Layers { get; set; } = new();
        public List<ObjectInstanceModel> Instances { get; set; } = new();
    }
}