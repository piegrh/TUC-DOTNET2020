using System;
using System.Diagnostics;
using System.Threading;
namespace Engine
{
    public static class App
    {
        /// <summary>
        /// Returns <see langword="True"/> if the game is running
        /// </summary>
        public static bool IsRunning { get; private set; } = false;
        /// <summary>
        /// Quit Application
        /// </summary>
        /// <param name="msg"></param>
        public static void Quit(string msg = "")
        {
            exitMessage = msg;
            IsRunning = false;
        }
        public static Action onAppExit;
        public static readonly Time time = new Time();
        public static Random rnd = new Random();
        static string exitMessage = "";
        static int frameRate = 10;
        static bool renderGame = true;
        static GameWorld world;
        static Renderer renderer;
        /// <summary>
        /// Run the game.
        /// </summary>
        /// <param name="w">World</param>
        /// <param name="RenderGame">Render game</param>
        public static bool Run(GameWorld w, bool RenderGame = true)
        {
            if (IsRunning)
                return false;
            renderGame = RenderGame;
            Init(w, out Stopwatch sw, out long start, out uint updateCounter);
            while (IsRunning)
            {
                updateCounter++;
                sw.Restart();
                Update();
                sw.Stop();
                ThreadSleep(sw);
            }
            PrintEndStats(start, updateCounter);
            onAppExit?.Invoke();
            world = null;
            renderer = null;
            return true;
        }
        static void ThreadSleep(Stopwatch sw)
        {
            float frameTime = MathF.Ceiling((1000.0f / frameRate) - sw.ElapsedMilliseconds);
            if (frameTime > 0)
                Thread.Sleep((int)frameTime);
        }
        static void Init(GameWorld w, out Stopwatch sw, out long start, out uint cnt)
        {
            IsRunning = true;
            time.Reset();
            world = w;
            if (renderGame)
            {
                renderer = new Renderer(w);
            }
            sw = new Stopwatch();
            start = DateTime.Now.Ticks;
            cnt = 0;
            Console.Clear();
        }
        static void PrintEndStats(long start, uint cnt)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (exitMessage != string.Empty)
            {
                Console.WriteLine(exitMessage);
            }
            exitMessage = string.Empty;
            float time = (DateTime.Now.Ticks - start) / 10_000_000;
            Console.WriteLine($"Stopped after: {time}s\nUpdate Count: {cnt}\nFPS: {cnt / time}");
        }
        static void HandleInput()
        {
            switch (Input.GetKey())
            {
                case ConsoleKey.Q:
                    Quit("Game aborted");
                    break;
                case ConsoleKey.PageUp:
                    frameRate = Utils.Clamp(++frameRate, 5, 100);
                    break;
                case ConsoleKey.PageDown:
                    frameRate = Utils.Clamp(--frameRate, 5, 100);
                    break;
            }
        }
        /// <summary>
        /// Main loop
        /// </summary>
        static void Update()
        {
            time.Update();
            Input.Update();
            HandleInput();
            if (renderGame)
            {
                renderer.RenderBlank();
            }
            world.Update();
            if (renderGame)
            {
                renderer.Render();
            }
        }
    }
}
