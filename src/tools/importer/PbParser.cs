using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tululoo.Tools.Importer
{
    public class PbParser
    {
        private readonly string _text;

        public PbParser(string pbContent)
        {
            _text = pbContent ?? throw new ArgumentNullException(nameof(pbContent));
        }

        public IEnumerable<string> GetStructures()
        {
            var matches = Regex.Matches(_text, @"Structure\s+([A-Za-z0-9_]+)\s*(.*?)EndStructure", RegexOptions.Singleline);
            foreach (Match m in matches)
                yield return m.Groups[1].Value;
        }

        public IEnumerable<string> GetGlobals()
        {
            var matches = Regex.Matches(_text, @"Global\s+.+", RegexOptions.Singleline);
            foreach (Match m in matches)
                yield return m.Value.Trim();
        }

        public IEnumerable<string> GetEnumerations()
        {
            var matches = Regex.Matches(_text, @"Enumeration\s*(.*?)EndEnumeration", RegexOptions.Singleline);
            foreach (Match m in matches)
                yield return m.Groups[1].Value.Trim();
        }
    }
}