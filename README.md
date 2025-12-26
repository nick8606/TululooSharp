```markdown
# TululooSharp

Fork / port of Tululoo (PowerBASIC) → C# (.NET 8), Avalonia UI.

Goals
- Reimplement Tululoo core and toolset on .NET 8
- Provide an importer that converts original .pb/.pbi projects into a stable JSON/C# model
- Keep game runtime language: JavaScript (game.js) — support web preview first
- Add optional scripting hosts: Lua (MoonSharp) and C# scripting (Roslyn)
- Later: native exports for Windows/Linux/macOS; research SGDK/Butano transpilations as a separate epic

Repository skeleton
- src/Tululoo.Core  — domain models and managers
- src/Tululoo.Scripting — scripting hosts (stubs)
- src/Tululoo.Tools.Importer — pb/.pbi scanner & parser & converter (stubs)
- tests/ — unit tests (to be added)

Workflow (current plan)
1. Parse .pb/.pbi files and extract data model (structures, globals, resource lists)
2. Convert structures -> C# POCOs (done in this skeleton)
3. Implement PbToModelConverter: import project data into GameModel (phase 1: metadata/resources)
4. Add serialization (JSON) for new project format
5. After importer stabilizes → start building Avalonia editor that consumes GameModel

How to run the importer (after adding files)
- Build: dotnet build
- The importer project is an exe stub; implementation will provide CLI to run imports:
  dotnet run --project src/Tululoo.Tools.Importer -- <project-root>

Contributing
- We will proceed feature-by-feature. The first PR contains only skeleton models and light importer utilities.
```