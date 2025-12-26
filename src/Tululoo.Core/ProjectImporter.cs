using System;
using System.IO;
using System.Text.Json;

namespace Tululoo.Core
{
    // Простая стартовая заглушка для импортёра старых проектов PowerBASIC
    public class ProjectImporter
    {
        public ProjectImporter() { }

        // Попытка загрузить старый проект (формат: набор файлов/директорий Tululoo)
        // Для .pb/.pbi — мы будем парсить тело и конвертировать в JSON-проект
        public Project ImportFromDirectory(string path)
        {
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(path);
            // TODO: рекурсивно найти .pb/.pbi, классифицировать и собрать ресурсы
            var proj = new Project { Name = Path.GetFileName(path), OriginalPath = path };
            return proj;
        }
    }

    public class Project
    {
        public string Name { get; set; } = "Untitled";
        public string OriginalPath { get; set; } = "";
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
        // далее: сцены, объекты, ресурсы
    }
}