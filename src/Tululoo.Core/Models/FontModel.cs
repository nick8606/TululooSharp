namespace Tululoo.Core.Models
{
    public class FontModel
    {
        public int UID { get; set; }
        public string Name { get; set; } = "";
        public string Family { get; set; } = "";
        public int Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public string Keywords { get; set; } = "";
    }
}