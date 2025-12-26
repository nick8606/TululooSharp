using System;
using Tululoo.Core.Models;

namespace Tululoo.Core
{
    // Small entry-point stub for running import tests and verifying models
    public class TululooMain
    {
        public GameModel Game { get; private set; }

        public TululooMain()
        {
            Game = new GameModel();
        }

        public void Initialize()
        {
            Console.WriteLine("TululooSharp core initialized.");
        }

        public void LoadDummy()
        {
            Game.Title = "Imported Tululoo Project (placeholder)";
        }

        public void Run()
        {
            Initialize();
            LoadDummy();
            Console.WriteLine($"Game title: {Game.Title}");
        }
    }
}