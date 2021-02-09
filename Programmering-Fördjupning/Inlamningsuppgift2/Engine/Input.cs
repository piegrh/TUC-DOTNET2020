using System;
namespace Engine
{
    public class Input
    {
        static ConsoleKey key;
        public static void Update()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
                return;
            }
            key = ConsoleKey.NoName;
        }
        /// <summary>
        /// Currently pressed key
        /// </summary>
        /// <param name="key">ConsoleKey</param>
        /// <returns></returns>
        public static ConsoleKey GetKey() => key;
    }
}
