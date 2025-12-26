namespace Tululoo.Core.Models
{
    public class ObjectModel
    {
        public int UID { get; set; }
        public string Name { get; set; } = "";
        public string Sprite { get; set; } = "";
        public bool Visible { get; set; } = true;
        public int Depth { get; set; }
        public string Parent { get; set; } = "";
        public string Code { get; set; } = "";
    }

    public class ObjectInstanceModel
    {
        public int InstanceId { get; set; }
        public string ObjectType { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Depth { get; set; }
    }
}