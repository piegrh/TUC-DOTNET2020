using Engine;
using Engine.Maps;
using Inlamningsuppgift2.Game;
using System;
namespace Inlamningsuppgift2
{
    class Program
    {
        enum MenuOption { Quick = 1, Custom, Exit };
        static readonly bool renderGame = true;
        static Vector2 mapSize = new Vector2(64, 32);
        static LevelGeneratorSettings settings = new LevelGeneratorSettings()
        {
            MaxIterations = 10000,
            MaxWalkerCount = 20,
            TargetFloorCoverage = 0.35f,
            KillProb = 0.1,
            SpawnProb = 0.5,
            ChangeDirectionProb = 0.20
        };
        public static void Main()
        {
            while (true)
            {
                Console.Title = $"NotSnake";
                Console.WriteLine("Menu");
                Console.WriteLine("1. Quick Start");
                Console.WriteLine("2. Custom Game");
                Console.WriteLine("3. Exit");
                bool player, randomMap;
                int food, ai, seed;
                switch ((MenuOption)PromptIntValue("", 1, 3))
                {
                    case MenuOption.Quick:
                        GetQuickStartSettings(out player, out randomMap, out food, out ai, out seed);
                        break;
                    case MenuOption.Custom:
                        GetCustomGameSettings(out player, out randomMap, out food, out ai, out seed);
                        break;
                    case MenuOption.Exit:
                        return;
                    default:
                        continue;
                }
                if (randomMap)
                {
                    Console.Title = $"NotSnake: seed {seed}";
                }
                Map level = randomMap ? MapGenerator.RandomDungeon(mapSize, settings, seed) : MapGenerator.SimpleRoom(mapSize);
                App.Run(new NotSnakeGame(level, foodCnt: food, AIPlayers: ai, humanPlayer: player), renderGame);
            }
        }
        static void GetCustomGameSettings(out bool player, out bool rndom, out int food, out int ai, out int seed)
        {
            player = PromptYesOrNo("Add human player");
            food = PromptIntValue("Food count", max: renderGame ? 50 : 99999);
            ai = PromptIntValue("AI count", max: renderGame ? 25 : 99999);
            rndom = PromptYesOrNo("Random generated map");
            seed = rndom ? PromptIntValue("Seed value", int.MinValue, int.MaxValue) : 0;
            if (!renderGame)
            {
                int x = PromptIntValue("Map width", max: 1000);
                int y = PromptIntValue("Map height", max: 1000);
                mapSize = new Vector2(x, y);
            }
        }
        static void GetQuickStartSettings(out bool player, out bool rndom, out int food, out int ai, out int seed)
        {
            player = true;
            food = 5;
            ai = 5;
            rndom = true;
            seed = App.rnd.Next(0, int.MaxValue);
        }
        static int PromptIntValue(string msg, int min = 0, int max = 100)
        {
            while (true)
            {
                if (msg != string.Empty)
                {
                    Console.WriteLine($"{msg}");
                }
                Console.Write(">");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (Utils.Between(value, min, max))
                    {
                        return value;
                    }
                    Console.WriteLine($"Value too {(value > min ? "high" : "low")}. [{min},{max}]");
                    continue;
                }
                Console.WriteLine("Invalid value.");
            }
        }
        static bool PromptYesOrNo(string msg)
        {
            Console.WriteLine($"{msg}? (y/n)");
            Console.Write(">");
            string s = Console.ReadLine();
            return s != string.Empty && s[0].ToString().ToLower() == "y";
        }
    }
}
