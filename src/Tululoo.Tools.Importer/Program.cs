using System;
using System.IO;
using Tululoo.Tools.Importer;

namespace Tululoo.Tools.Importer
{
    // CLI для импорта: dotnet run --project src/Tululoo.Tools.Importer -- import <project-root> [--out <out.json>]
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 2 || args[0].ToLowerInvariant() != "import")
            {
                Console.WriteLine("Usage: import <project-root> [--out <out.json>]");
                return 1;
            }

            var projectRoot = args[1];
            var outFile = Path.Combine(projectRoot, "project.json");

            for (int i = 2; i < args.Length; i++)
            {
                if (args[i] == "--out" && i + 1 < args.Length)
                {
                    outFile = args[i + 1];
                    i++;
                }
            }

            if (!Directory.Exists(projectRoot))
            {
                Console.WriteLine("Project root not found: " + projectRoot);
                return 2;
            }

            try
            {
                var converter = new PbToModelConverter();
                var game = converter.ConvertFromProjectFolder(projectRoot);
                converter.SaveGameModel(game, outFile);
                Console.WriteLine("Imported project -> " + outFile);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Import failed: " + ex);
                return 3;
            }
        }
    }
}