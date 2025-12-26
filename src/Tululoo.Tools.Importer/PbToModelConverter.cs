using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Tululoo.Core.Models;

namespace Tululoo.Tools.Importer
{
    // Минимальная реализация конвертера: собирает ассеты (img/, aud/, mus/, fonts),
    // создает простую модель GameModel и сохраняет её в project.json.
    // Этот конвертер не пытается полноценно парсить всю PB-логику — он делает фазовый импорт данных,
    // который позволит позже собирать UI и переписывать поведение.
    public class PbToModelConverter
    {
        public GameModel ConvertFromProjectFolder(string projectRoot)
        {
            if (string.IsNullOrEmpty(projectRoot)) throw new ArgumentNullException(nameof(projectRoot));
            if (!Directory.Exists(projectRoot)) throw new DirectoryNotFoundException(projectRoot);

            var game = new GameModel();
            game.Title = Path.GetFileName(projectRoot);

            // Find assets folders (common Tululoo layout: img/, aud/, music/, fonts/)
            var imgDir = Path.Combine(projectRoot, "img");
            var audDir = Path.Combine(projectRoot, "aud");
            var musicDir = Path.Combine(projectRoot, "mus");
            var fontDir = Path.Combine(projectRoot, "fonts");

            var uid = 1;
            var uidProvider = new Tululoo.Core.Managers.UidProvider(1);

            // Import sprites from img/ (each file becomes a Frame; grouping by filename prefix)
            if (Directory.Exists(imgDir))
            {
                var images = Directory.EnumerateFiles(imgDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => IsImageFile(f)).ToList();

                // Simple grouping: treat each file as a separate Sprite with single frame
                foreach (var file in images)
                {
                    var sprite = new SpriteModel
                    {
                        Name = Path.GetFileNameWithoutExtension(file),
                        UID = uidProvider.Next(),
                        Frames = new List<FrameModel>
                        {
                            new FrameModel { File = MakeRelativePath(file, projectRoot), UID = uidProvider.Next() }
                        }
                    };
                    game.Sprites.Add(sprite);
                }
            }

            // Import sounds
            if (Directory.Exists(audDir))
            {
                var sounds = Directory.EnumerateFiles(audDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => IsAudioFile(f)).ToList();
                foreach (var file in sounds)
                {
                    var sound = new SoundModel
                    {
                        UID = uidProvider.Next(),
                        Name = Path.GetFileNameWithoutExtension(file),
                        File = MakeRelativePath(file, projectRoot)
                    };
                    game.Sounds.Add(sound);
                }
            }

            // Import music (if any)
            if (Directory.Exists(musicDir))
            {
                var mus = Directory.EnumerateFiles(musicDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => IsAudioFile(f)).ToList();
                foreach (var file in mus)
                {
                    var m = new MusicModel
                    {
                        UID = uidProvider.Next(),
                        Name = Path.GetFileNameWithoutExtension(file),
                        File = MakeRelativePath(file, projectRoot)
                    };
                    game.Musics.Add(m);
                }
            }

            // Import fonts (basic)
            if (Directory.Exists(fontDir))
            {
                var fonts = Directory.EnumerateFiles(fontDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => f.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".otf", StringComparison.OrdinalIgnoreCase));
                foreach (var file in fonts)
                {
                    var font = new FontModel
                    {
                        UID = uidProvider.Next(),
                        Name = Path.GetFileNameWithoutExtension(file),
                        Family = Path.GetFileNameWithoutExtension(file)
                    };
                    game.Fonts.Add(font);
                }
            }

            // Look for a tululoo.pb in root (legacy) and parse metadata from it
            var pbPath = Path.Combine(projectRoot, "tululoo.pb");
            if (!File.Exists(pbPath))
            {
                // If there's a top-level tululoo.pb elsewhere (repo root), try that
                var alt = Directory.EnumerateFiles(projectRoot, "tululoo.pb", SearchOption.AllDirectories).FirstOrDefault();
                if (alt != null) pbPath = alt;
            }

            if (File.Exists(pbPath))
            {
                var pbText = File.ReadAllText(pbPath);
                var parser = new PbParser(pbText);

                // override title if present in globals
                foreach (var g in parser.GetGlobals())
                {
                    if (g.StartsWith("ProjectName", StringComparison.OrdinalIgnoreCase))
                    {
                        // Very naive extraction: ProjectName.s = "name"
                        var m = System.Text.RegularExpressions.Regex.Match(g, @"ProjectName\.(?:s|S)?\s*=\s*""([^""]+)""");
                        if (m.Success)
                            game.Title = m.Groups[1].Value;
                    }
                }

                // Extract declared structures names: useful for mapping in future
                foreach (var s in parser.GetStructures())
                {
                    // We don't auto-create models from these names here; models exist already.
                    // But we could log or store the list for the importer report.
                }
            }

            return game;
        }

        private static string MakeRelativePath(string fullPath, string root)
        {
            var rp = Path.GetRelativePath(root, fullPath).Replace('\\', '/');
            return rp;
        }

        private static bool IsImageFile(string file)
        {
            var ext = Path.GetExtension(file).ToLowerInvariant();
            return ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif";
        }

        private static bool IsAudioFile(string file)
        {
            var ext = Path.GetExtension(file).ToLowerInvariant();
            return ext == ".wav" || ext == ".ogg" || ext == ".mp3" || ext == ".flac";
        }

        public void SaveGameModel(GameModel model, string outputFile)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(model, options);
            File.WriteAllText(outputFile, json);
        }
    }
}