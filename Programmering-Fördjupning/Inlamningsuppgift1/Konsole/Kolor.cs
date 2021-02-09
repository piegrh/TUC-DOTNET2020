using System;
using System.Text;
namespace Pierre
{
    public static class Kolor
    {
        static readonly ConsoleColor[] colors;
        static readonly Random rnd;
        static Kolor()
        {
            colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            rnd = new Random();
        }
        #region Color Stuff
        public const int ColorCount = 16;
        public const char ColorEscape = '^';
        public const char ColorBlack = '0';
        public const char ColorRed = '1';
        public const char ColorGreen = '2';
        public const char ColorYellow = '3';
        public const char ColorBlue = '4';
        public const char ColorCyan = '5';
        public const char ColorMagenta = '6';
        public const char ColorWhite = '7';
        public const char ColorGray = '8';
        public const char ColorDarkGray = '9';
        public const char ColorDarkBlue = 'A';
        public const char ColorDarkGreen = 'B';
        public const char ColorDarkCyan = 'C';
        public const char ColorDarkRed = 'D';
        public const char ColorDarkMagenta = 'E';
        public const char ColorDarkYellow = 'F';
        public const char ColorBackground = '£';
        public const char ColorForeground = '$';
        public const char ColorRandom = 'R';
        static ConsoleColor RandomColor() => colors[rnd.Next(1, ColorCount)];
        public const string Default = BackgroundKolor.Default + ForegroundKolor.Default;
        public const string Inverted = BackgroundKolor.Foreground + ForegroundKolor.Background;
        #endregion
        /// <summary>
        /// Checks if <paramref name="text"/> starts with an escape sequence.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool StartsWithColorEscapeSequence(string text) => text.Length >= 2 && text[0] == ColorEscape && (char.IsLetterOrDigit(text[1]) || IsReservedChar(text[1]));
        /// <summary>
        /// Checks if <paramref name="text"/> with the background color escape sequence
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool StartsWithBGColorEscapeSequence(string text) => text.Length >= 3 && text[0] == ColorEscape && text[1] == ColorEscape && (char.IsLetterOrDigit(text[1]) || IsReservedChar(text[2]));
        /// <summary>
        /// Check if <paramref name="text"/> contains any color escape sequences.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsColorString(string text) => !EscapeColors(text).Equals(text);
        /// <summary>
        /// Removes all color escape sequences from <paramref name="text"/>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EscapeColors(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < text.Length)
            {
                string temp = text.Substring(i, text.Length - i);
                if (StartsWithBGColorEscapeSequence(temp))
                {
                    i += 3;
                    continue;
                }
                if (StartsWithColorEscapeSequence(temp))
                {
                    i += 2;
                    continue;
                }
                sb.Append(text[i++]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Returns the <see cref="ConsoleColor"/> that is mapped to <paramref name="c"/>.
        /// '1' = red, '2' = green, '3' = yellow .... 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static ConsoleColor ParseColor(char c)
        {
            switch (char.ToUpper(c))
            {
                case ColorBlack: return ConsoleColor.Black;
                case ColorDarkBlue: return ConsoleColor.DarkBlue;
                case ColorDarkGreen: return ConsoleColor.DarkGreen;
                case ColorDarkCyan: return ConsoleColor.DarkCyan;
                case ColorDarkRed: return ConsoleColor.DarkRed;
                case ColorDarkMagenta: return ConsoleColor.DarkMagenta;
                case ColorDarkYellow: return ConsoleColor.DarkYellow;
                case ColorGray: return ConsoleColor.Gray;
                case ColorDarkGray: return ConsoleColor.DarkGray;
                case ColorBlue: return ConsoleColor.Blue;
                case ColorGreen: return ConsoleColor.Green;
                case ColorCyan: return ConsoleColor.Cyan;
                case ColorRed: return ConsoleColor.Red;
                case ColorMagenta: return ConsoleColor.Magenta;
                case ColorYellow: return ConsoleColor.Yellow;
                case ColorWhite: return ConsoleColor.White;
                case ColorRandom: return RandomColor();
                case ColorBackground: return Konsole.DefaultBGColor;
                case ColorForeground: return Konsole.DefaultFGColor;
                default:
                    return ParseColor(ColorGray);
            }
        }
        /// <summary>
        /// Checks if <paramref name="c"/> is a reserved <see langword=""="char"/>.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsReservedChar(char c)
        {
            switch (char.ToUpper(c))
            {
                case ColorBlack: break;
                case ColorDarkBlue: break;
                case ColorDarkGreen: break;
                case ColorDarkCyan: break;
                case ColorDarkRed: break;
                case ColorDarkMagenta: break;
                case ColorDarkYellow: break;
                case ColorGray: break;
                case ColorDarkGray: break;
                case ColorBlue: break;
                case ColorGreen: break;
                case ColorCyan: break;
                case ColorRed: break;
                case ColorMagenta: break;
                case ColorYellow: break;
                case ColorWhite: break;
                case ColorRandom: break;
                case ColorBackground: break;
                case ColorForeground: break;
                default:
                    return false;
            }
            return true;
        }
    }
}
