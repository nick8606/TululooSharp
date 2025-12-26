using System.Collections.Generic;

namespace Tululoo.Core.Models
{
    public class GameModel
    {
        public string Title { get; set; } = "";
        public string Globals { get; set; } = "";
        public string Functions { get; set; } = "";
        public int ScriptMode { get; set; }
        public string GameComment { get; set; } = "";

        // UI-related IDs from original PB are NOT stored here; UI keeps its own mapping.
        // Collections
        public List<SpriteModel> Sprites { get; set; } = new();
        public List<SceneModel> Scenes { get; set; } = new();
        public List<ObjectModel> Objects { get; set; } = new();
        public List<SoundModel> Sounds { get; set; } = new();
        public List<MusicModel> Musics { get; set; } = new();
        public List<FontModel> Fonts { get; set; } = new();
        public List<FunctionModel> FunctionsList { get; set; } = new();
        public List<ScriptModel> Scripts { get; set; } = new();

        // Metadata
        public int UIDCounter { get; set; } = 1;
    }
}