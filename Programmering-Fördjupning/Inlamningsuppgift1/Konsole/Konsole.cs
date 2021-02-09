using System;

namespace Pierre
{
    public class Konsole
    {
        Konsole() { }
        static Konsole()
        {
            DefaultBGColor = Console.BackgroundColor;
            DefaultFGColor = Console.ForegroundColor;
        }
        static ConsoleColor _Bg = ConsoleColor.Black;
        /// <summary>
        /// Background color of the console
        /// </summary>
        public static ConsoleColor DefaultBGColor
        {
            get
            {
                return _Bg;
            }
            set
            {
                Console.BackgroundColor = _Bg;
                _Bg = value;
            }
        }
        static ConsoleColor _Fg = ConsoleColor.Gray;
        /// <summary>
        /// Foreground color of the console
        /// </summary>
        public static ConsoleColor DefaultFGColor
        {
            get
            {
                return _Fg;
            }
            set
            {
                Console.ForegroundColor = _Fg;
                _Fg = value;
            }
        }
        public static void WriteLine() => Console.WriteLine();
        /// <summary>
        /// Similar to "Console.WriteLine" but with support for Quake3 style color formating.
        /// </summary>
        /// <example>
        /// <code>
        /// // Two ways of achieving the same thing
        /// Konsole.WriteLine("^^1^0BLACK TEXT RED BACKGROUND");
        /// Konsole.WriteLine(Kolor.BGRED, Kolor.BLACK, "BLACK TEXT RED BACKGROUND")
        /// </code>
        /// </example>
        /// <param name="line"></param>
        public static void WriteLine(params object[] line)
        {
            Write(line);
            Write(Environment.NewLine);
        }
        private static void Write(string text)
        {
            int i = 0;
            while (i < text.Length)
            {
                if (Kolor.StartsWithBGColorEscapeSequence(text.Substring(i, text.Length - i)))
                {
                    Console.BackgroundColor = Kolor.ParseColor(text[i + 2]);
                    i += 3;
                }
                if (Kolor.StartsWithColorEscapeSequence(text.Substring(i, text.Length - i)))
                {
                    Console.ForegroundColor = Kolor.ParseColor(text[i + 1]);
                    i += 2;
                }
                if (i + 1 > text.Length)
                    break;
                Console.Write(text[i++]);
            }
        }
        /// <summary>
        /// Similar to "Console.Write" but with support for Quake3 color formating.
        /// </summary>
        /// <example>
        /// <code>
        /// // Two ways of achieving the same thing:
        /// Konsole.Write("^^1^0BLACK TEXT RED BACKGROUND");
        /// Konsole.Write(Kolor.BGRED, Kolor.BLACK, "BLACK TEXT RED BACKGROUND")
        /// </code>
        /// </example>
        /// <param name="line"></param>
        public static void Write(params object[] input)
        {
            if (input == null)
                return;
            for (int j = 0; j < input.Length; j++)
            {
                Write(input[j].ToString());
            }
            Console.BackgroundColor = DefaultBGColor;
            Console.ForegroundColor = DefaultFGColor;
        }
    }
}