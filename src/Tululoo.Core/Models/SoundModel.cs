namespace Tululoo.Core.Models
{
    public class SoundModel
    {
        public int UID { get; set; }
        public string Name { get; set; } = "";
        public string File { get; set; } = "";
    }

    public class MusicModel : SoundModel { }
}