using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tululoo.Tools.Importer
{
    // Улучшённый PB-парсер, извлекает структуры, globals, includes, declare и тела процедур.
    // Регулярные выражения используются для быстрой инвентаризации; это НЕ полноценный PB-парсер,
    // но достаточно для извлечения метаданных и разделения процедуры/блоков для ручной конвертации.
    public class PbParser
    {
        private readonly string _text;

        public PbParser(string pbContent)
        {
            _text = pbContent ?? throw new ArgumentNullException(nameof(pbContent));
        }

        public IEnumerable<string> GetStructures()
        {
            var matches = Regex.Matches(_text, @"(?is)Structure\s+([A-Za-z0-9_]+)\s*(.*?)EndStructure");
            foreach (Match m in matches)
                yield return m.Groups[1].Value;
        }

        public IEnumerable<string> GetGlobals()
        {
            var matches = Regex.Matches(_text, @"(?im)^\s*Global\s+(.+)$");
            foreach (Match m in matches)
                yield return m.Groups[1].Value.Trim();
        }

        public IEnumerable<string> GetEnumerations()
        {
            var matches = Regex.Matches(_text, @"(?is)Enumeration\s*(.*?)EndEnumeration");
            foreach (Match m in matches)
                yield return m.Groups[1].Value.Trim();
        }

        public IEnumerable<string> GetIncludeFiles()
        {
            var matches = Regex.Matches(_text, @"(?i)(?:IncludeFile|XIncludeFile)\s+""([^""]+)""");
            foreach (Match m in matches)
                yield return m.Groups[1].Value.Trim();
        }

        public IEnumerable<string> GetDeclares()
        {
            var matches = Regex.Matches(_text, @"(?im)^\s*Declare\..*$|^\s*Declare\s+.+$");
            foreach (Match m in matches)
                yield return m.Value.Trim();
        }

        // Получает имена процедур (Procedure <name> или ProcedureName: forms) и также извлекает тела
        public IEnumerable<(string Name, string Body)> GetProcedures()
        {
            // Находим все Procedure ... EndProcedure блоки
            var procMatches = Regex.Matches(_text, @"(?is)Procedure(?:\.\w+)?\s+([A-Za-z0-9_]+)\s*(.*?)EndProcedure");
            foreach (Match m in procMatches)
            {
                yield return (m.Groups[1].Value, m.Groups[2].Value.Trim());
            }

            // Также рассмотрим Function ... EndProcedure (PowerBASIC может использовать Function or Procedure)
            var funcMatches = Regex.Matches(_text, @"(?is)Procedure(?:\.\w+)?\s+([A-Za-z0-9_]+)\s*(.*?)EndProcedure");
            foreach (Match m in funcMatches)
                yield return (m.Groups[1].Value, m.Groups[2].Value.Trim());
        }

        // Быстрая выдача имён процедур (без тел)
        public IEnumerable<string> GetProcedureNames()
        {
            foreach (var (Name, _) in GetProcedures())
                yield return Name;
        }
    }
}