using System;
using MoonSharp.Interpreter;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Tululoo.Scripting
{
    public class ScriptingHost
    {
        public Script? LuaScript { get; private set; }

        public void InitLua()
        {
            LuaScript = new Script();
            // Регистрация API
        }

        public void RunCSharpScript(string code)
        {
            var state = CSharpScript.RunAsync(code, ScriptOptions.Default).GetAwaiter().GetResult();
            // TODO: безопасный sandboxing — требует ограничений
        }
    }
}