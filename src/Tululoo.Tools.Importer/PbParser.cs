using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tululoo.Tools.Importer
{
    // Lightweight parser to extract top-level metadata from .pb files:
    // - Structure names
    // - Global declarations
    // - Enumerations
    // - IncludeFile / XIncludeFile lines
    public class PbParser
    {
        private readonly string _text;

        public PbParser(string pbContent)
        {
            _text = pbContent ?? throw new ArgumentNullException(nameof(pbContent));
        }

        public IEnumerable<string> GetStructures()
        {
            var matches = Regex.Matches(_text, @"Structure\s+([A-Za-z0-9_]+)\s*(.*?)EndStructure", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match m in matches)
                yield return m.Groups[1].Value;
        }

        public IEnumerable<string> GetGlobals()
        {
            var matches = Regex.Matches(_text, @"^\s*Global\s+.+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match m in matches)
                yield return m.Value.Trim();
        }

        public IEnumerable<string> GetEnumerations()
        {
            var matches = Regex.Matches(_text, @"Enumeration\s*(.*?)EndEnumeration", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match m in matches)
                yield return m.Groups[1].Value.Trim();
        }

        public IEnumerable<string> GetIncludeFiles()
        {
            var matches = Regex.Matches(_text, @"(?:IncludeFile|XIncludeFile)\s+""([^""]+)""", RegexOptions.IgnoreCase);
            foreach (Match m in matches)
                yield return m.Groups[1].Value;
        }

        // Simple extractor of procedure names (Declare / Procedure)
        public IEnumerable<string> GetProcedures()
        {
            var matches = Regex.Matches(_text, @"(?:Declare\.[^\n]*|Procedure(?:\.\w+)?\s+([A-Za-z0-9_]+))", RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                if (!string.IsNullOrEmpty(m.Groups[1].Value))
                    yield return m.Groups[1].Value;
            }
        }
    }
}