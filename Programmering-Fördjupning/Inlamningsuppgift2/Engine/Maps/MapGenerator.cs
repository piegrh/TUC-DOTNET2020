using System;
using System.Collections.Generic;
using static Engine.Maps.Map;

namespace Engine.Maps
{
    public struct LevelGeneratorSettings
    {
        public int MaxIterations { get; set; }
        public int MaxWalkerCount { get; set; }
        public float TargetFloorCoverage { get; set; }
        public double SpawnProb { get; set; }
        public double KillProb { get; set; }
        public double ChangeDirectionProb { get; set; }
    }
    /// <summary>
    /// A class that generates <see cref="Map"/>s.
    /// </summary>
    public static class MapGenerator
    {
        class Walker
        {
            public Direction Direction { get; set; }
            public Vector2 Position { get; set; }
        }
        /// <summary>
        /// Generates an empty <see cref="Map"/>
        /// </summary>
        /// <param name="size">Size of the <see cref="Map"/></param>
        /// <returns></returns>
        public static Map EmptyRoom(Vector2 size)
        {
            Map map = new Map(size);
            return map;
        }
        /// <summary>
        /// Generates an empty <see cref="Map"/> with walls
        /// </summary>
        /// <param name="size">Size of the <see cref="Map"/></param>
        /// <returns></returns>
        public static Map SimpleRoom(Vector2 size)
        {
            Map map = new Map(size);
            map.DrawBox(Vector2.Zero, new Vector2(map.Size), Node.WALL);
            map.DrawBox(new Vector2(1, 1), new Vector2(map.Size - new Vector2(2, 2)), Node.FLOOR);
            return map;
        }
        /// <summary>
        /// Generates a <see cref="Map"/> from a 2D array of ints. 
        /// <para>Positive numbers are walkable and negative numbers are walls.</para>
        /// </summary>
        /// <param name="arr">2D Array of ints</param>
        /// <returns></returns>
        public static Map CreateFrom2DArray(int[,] arr)
        {
            Map l = new Map(new Vector2(arr.GetLength(0), arr.GetLength(1)));
            for (int y = 0; y < l.Size.y; y++)
            {
                for (int x = 0; x < l.Size.x; x++)
                {
                    l.Nodes[x, y] = new Node(new Vector2(x, y), arr[x, y]);
                }
            }
            return l;
        }
        /// <summary>
        /// Generates a random "dungeon" <see cref="Map"/>
        /// </summary>
        /// <param name="size">Size of the <see cref="Map"/></param>
        /// <param name="seed">Random number generator seed</param>
        /// <returns></returns>
        public static Map RandomDungeon(Vector2 size, int seed = 123)
        {
            LevelGeneratorSettings settings = new LevelGeneratorSettings()
            {
                MaxIterations = 10000,
                MaxWalkerCount = 20,
                TargetFloorCoverage = 0.75f,
                KillProb = 0.1,
                SpawnProb = 0.2,
                ChangeDirectionProb = 0.2
            };
            return RandomDungeon(size, settings, seed);
        }
        /// <summary>
        /// Generates a random "dungeon" <see cref="Map"/>
        /// </summary>
        /// <param name="size">Size of the <see cref="Map"/></param>
        /// <param name="settings">Level generator settings</param>
        /// <param name="seed">Random number generator seed</param>
        /// <returns></returns>
        public static Map RandomDungeon(Vector2 size, LevelGeneratorSettings settings, int seed = 123)
        {
            Random rand = new Random(seed);
            Map map = new Map(size);
            map.DrawBox(Vector2.Zero, new Vector2(map.Size), Node.WALL);
            List<Walker> walkers = new List<Walker>
            {
                new Walker() { Position = new Vector2(map.Size / 2) }
            };
            int cnt = 0;
            // Main loop
            while (true)
            {
                if (MapCovargeIsDone(map, settings.TargetFloorCoverage))
                {
                    break;
                }
                walkers.KillWalkers(settings.KillProb, rand);
                walkers.SpawnWalkers(settings.MaxWalkerCount, settings.SpawnProb, rand);
                walkers.Walk(map, settings.ChangeDirectionProb, rand);
                if (cnt++ >= settings.MaxIterations)
                {
                    break;
                }
            }
            return map;
        }
        static bool MapCovargeIsDone(Map map, float targetFloorCoverage)
        {
            float totalNodeCount = map.Size.x * map.Size.y;
            int cnt = CountFloorNodes(map);
            return cnt / totalNodeCount >= targetFloorCoverage;
        }
        static int CountFloorNodes(Map map)
        {
            int cnt = 0;
            for (int y = 0; y < map.Size.y; y++)
            {
                for (int x = 0; x < map.Size.x; x++)
                {
                    if (map.Nodes[x, y].IsFloor)
                    {
                        cnt++;
                    }
                }
            }
            return cnt;
        }
        static void KillWalkers(this List<Walker> walkers, double killProb, Random rnd)
        {
            if (walkers.Count > 1)
            {
                for (int i = walkers.Count - 1; i > 0; i--)
                {
                    if (rnd.NextDouble() < killProb)
                    {
                        walkers.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        static void SpawnWalkers(this List<Walker> walkers, int maxWalkerCount, double spawnProb, Random rnd)
        {
            for (int i = walkers.Count - 1; i > 0; i--)
            {
                if (walkers.Count < maxWalkerCount)
                {
                    if (rnd.NextDouble() < spawnProb)
                    {
                        walkers.Add(new Walker()
                        {
                            Position = walkers[i].Position,
                            Direction = (Direction)rnd.Next(1, 5)
                        });
                    }
                    continue;
                }
                break;
            }
        }
        static void Walk(this List<Walker> walkers, Map lvl, double changeDirectionProb, Random rnd)
        {
            foreach (Walker walker in walkers)
            {
                if (rnd.NextDouble() < changeDirectionProb)
                {
                    walker.Direction = (Direction)rnd.Next(1, 5);
                }
                Vector2 newPos = walker.Position + Utils.DirectionToVector2(walker.Direction);
                Utils.Clamp(ref newPos, new Vector2(2, 2), lvl.Size - new Vector2(2, 2));
                if (lvl.TryGetNode(newPos, out _))
                {
                    lvl.Nodes[newPos.x, newPos.y].NodeType = Node.FLOOR;
                    walker.Position = newPos;
                }
            }
        }
        static void DrawBox(this Map lvl, Vector2 pos, Vector2 size, int nodeType)
        {
            size = new Vector2(pos.x + size.x, pos.y + size.y);
            for (int x = pos.x; x < size.x; x++)
            {
                for (int y = pos.y; y < size.y; y++)
                {
                    lvl.Nodes[x, y].NodeType = nodeType;
                }
            }
        }
    }
}
