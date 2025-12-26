using System.Collections.Generic;
using System.IO;

namespace Tululoo.Tools.Importer
{
    public static class PbFileScanner
    {
        public static IEnumerable<string> FindPbFiles(string root)
        {
            if (!Directory.Exists(root)) yield break;
            foreach (var f in Directory.EnumerateFiles(root, "*.pb", SearchOption.AllDirectories))
                yield return f;
            foreach (var f in Directory.EnumerateFiles(root, "*.pbi", SearchOption.AllDirectories))
                yield return f;
        }
    }
}