using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konsole.UnitTests
{
    [TestClass]
    public class KonsoleUnitTests
    {
        [DataTestMethod]
        [DataRow("^1LOL")]
        [DataRow("^^0LOL")]
        [DataRow("L^1OL")]
        [DataRow("L^^0OL")]
        [DataRow("LOL^1")]
        [DataRow("LOL^^£")]
        [DataRow("LOL^$")]
        [DataRow("^1")]
        [DataRow("^^1")]
        public void IsColorString_IsTrue(string value)
        {
            Assert.IsTrue(Pierre.Kolor.IsColorString(value));
        }

        [DataTestMethod]
        [DataRow("123")]
        [DataRow("^")]
        [DataRow("^^")]
        [DataRow("^^^")]
        [DataRow("^^^^")]
        [DataRow("")]
        public void IsColorString_IsFalse(string value)
        {
            Assert.IsFalse(Pierre.Kolor.IsColorString(value));
        }

        [DataTestMethod]
        [DataRow("^1Yes")]
        [DataRow("^rYes")]
        [DataRow("^$Yes")]
        [DataRow("^£Yes")]
        public void StartsWithColorEscapeSequence_IsTrue(string value)
        {
            Assert.IsTrue(Pierre.Kolor.StartsWithColorEscapeSequence(value));
        }

        [DataTestMethod]
        [DataRow("^^0LOL")]
        [DataRow("^^rLOL")]
        [DataRow("^^$LOL")]
        [DataRow("^^£LOL")]
        public void StartsWithBGColorEscapeSequence_IsTrue(string value)
        {
            Assert.IsTrue(Pierre.Kolor.StartsWithBGColorEscapeSequence(value));
        }

        [DataTestMethod]
        [DataRow("L^1OL")]
        [DataRow("L^1OL^1")]
        [DataRow("^^1LOL")]
        public void StartsWithColorEscapeSequence_IsFalse(string value)
        {
            Assert.IsFalse(Pierre.Kolor.StartsWithColorEscapeSequence(value));
        }

        [DataTestMethod]
        [DataRow("^1LOL")]
        [DataRow("L^^0OL")]
        [DataRow("LOL^^0")]
        public void StartsWithBGColorEscapeSequence_IsFalse(string value)
        {
            Assert.IsFalse(Pierre.Kolor.StartsWithBGColorEscapeSequence(value));
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("^1", "")]
        [DataRow("^^1", "")]
        [DataRow("^1LOL","LOL")]
        [DataRow("^1L^1OL", "LOL")]
        [DataRow("^1L^1OL^1", "LOL")]
        [DataRow("^^A^1L^1OL^1", "LOL")]
        [DataRow("^^A^1L^1OL^1^^b", "LOL")]
        [DataRow("^1A^2N^3A^4R^5K^6I", "ANARKI")]
        public void EscapeColors_ColorString(string value,string expected)
        {
            string result = Pierre.Kolor.EscapeColors(value);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow("^^^^", "^^^^")]
        [DataRow("asdgagdagdgads", "asdgagdagdgads")]
        public void EscapeColors_NormalString(string value,string expected)
        {
            string result = Pierre.Kolor.EscapeColors(value);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(Pierre.Kolor.ColorBlack, Pierre.ForegroundKolor.Black)]
        [DataRow(Pierre.Kolor.ColorRed, Pierre.ForegroundKolor.Red)]
        [DataRow(Pierre.Kolor.ColorGreen, Pierre.ForegroundKolor.Green)]
        [DataRow(Pierre.Kolor.ColorYellow, Pierre.ForegroundKolor.Yellow)]
        [DataRow(Pierre.Kolor.ColorBlue, Pierre.ForegroundKolor.Blue)]
        [DataRow(Pierre.Kolor.ColorCyan, Pierre.ForegroundKolor.Cyan)]
        [DataRow(Pierre.Kolor.ColorMagenta, Pierre.ForegroundKolor.Magenta)]
        [DataRow(Pierre.Kolor.ColorWhite, Pierre.ForegroundKolor.White)]
        [DataRow(Pierre.Kolor.ColorGray, Pierre.ForegroundKolor.Gray)]
        [DataRow(Pierre.Kolor.ColorDarkGray, Pierre.ForegroundKolor.DarkGray)]
        [DataRow(Pierre.Kolor.ColorDarkBlue, Pierre.ForegroundKolor.DarkBlue)]
        [DataRow(Pierre.Kolor.ColorDarkGreen, Pierre.ForegroundKolor.DarkGreen)]
        [DataRow(Pierre.Kolor.ColorDarkCyan, Pierre.ForegroundKolor.DarkCyan)]
        [DataRow(Pierre.Kolor.ColorDarkRed, Pierre.ForegroundKolor.DarkRed)]
        [DataRow(Pierre.Kolor.ColorDarkMagenta, Pierre.ForegroundKolor.DarkMagenta)]
        [DataRow(Pierre.Kolor.ColorDarkYellow, Pierre.ForegroundKolor.DarkYellow)]
        [DataRow(Pierre.Kolor.ColorBackground, Pierre.ForegroundKolor.Background)]
        [DataRow(Pierre.Kolor.ColorForeground, Pierre.ForegroundKolor.Default)]
        [DataRow(Pierre.Kolor.ColorRandom, Pierre.ForegroundKolor.Random)]
        public void TestForegroundColors(char color, string expected)
        {
            string result = $"{Pierre.Kolor.ColorEscape}{color}";
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(Pierre.Kolor.ColorBlack, Pierre.BackgroundKolor.Black)]
        [DataRow(Pierre.Kolor.ColorRed, Pierre.BackgroundKolor.Red)]
        [DataRow(Pierre.Kolor.ColorGreen, Pierre.BackgroundKolor.Green)]
        [DataRow(Pierre.Kolor.ColorYellow, Pierre.BackgroundKolor.Yellow)]
        [DataRow(Pierre.Kolor.ColorBlue, Pierre.BackgroundKolor.Blue)]
        [DataRow(Pierre.Kolor.ColorCyan, Pierre.BackgroundKolor.Cyan)]
        [DataRow(Pierre.Kolor.ColorMagenta, Pierre.BackgroundKolor.Magenta)]
        [DataRow(Pierre.Kolor.ColorWhite, Pierre.BackgroundKolor.White)]
        [DataRow(Pierre.Kolor.ColorGray, Pierre.BackgroundKolor.Gray)]
        [DataRow(Pierre.Kolor.ColorDarkGray, Pierre.BackgroundKolor.DarkGray)]
        [DataRow(Pierre.Kolor.ColorDarkBlue, Pierre.BackgroundKolor.DarkBlue)]
        [DataRow(Pierre.Kolor.ColorDarkGreen, Pierre.BackgroundKolor.DarkGreen)]
        [DataRow(Pierre.Kolor.ColorDarkCyan, Pierre.BackgroundKolor.DarkCyan)]
        [DataRow(Pierre.Kolor.ColorDarkRed, Pierre.BackgroundKolor.DarkRed)]
        [DataRow(Pierre.Kolor.ColorDarkMagenta, Pierre.BackgroundKolor.DarkMagenta)]
        [DataRow(Pierre.Kolor.ColorDarkYellow, Pierre.BackgroundKolor.DarkYellow)]
        [DataRow(Pierre.Kolor.ColorBackground, Pierre.BackgroundKolor.Default)]
        [DataRow(Pierre.Kolor.ColorForeground, Pierre.BackgroundKolor.Foreground)]
        [DataRow(Pierre.Kolor.ColorRandom, Pierre.BackgroundKolor.Random)]
        public void TestBackgroundColors(char color, string expected)
        {
            string result = $"{Pierre.Kolor.ColorEscape}{Pierre.Kolor.ColorEscape}{color}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BackGroundColor()
        {
            Assert.AreEqual(Pierre.Konsole.DefaultBGColor, Console.BackgroundColor);
            // Make sure that ParseColor returns the current background color
            Assert.AreEqual(Pierre.Kolor.ParseColor(Pierre.Kolor.ColorBackground), Console.BackgroundColor);
            // Change background color to red
            Pierre.Konsole.DefaultBGColor = ConsoleColor.Red;
            // write sometext to the console with a different background color
            Pierre.Konsole.WriteLine(
                Pierre.BackgroundKolor.Cyan,
                Pierre.ForegroundKolor.Yellow,
                "ColorsShouldBeBackToNormalAfterThis");
            // Check if everything is back to normal (red background color)
            Assert.AreEqual(Pierre.Konsole.DefaultBGColor, ConsoleColor.Red);
            Assert.AreEqual(Pierre.Kolor.ParseColor(Pierre.Kolor.ColorBackground), ConsoleColor.Red);
        }

        [TestMethod]
        public void ForegroundColors()
        {
            Assert.AreEqual(Pierre.Konsole.DefaultFGColor, Console.ForegroundColor);
            Assert.AreEqual(Pierre.Kolor.ParseColor(Pierre.Kolor.ColorForeground), Console.ForegroundColor);
            Pierre.Konsole.DefaultFGColor = ConsoleColor.Green;
            Pierre.Konsole.WriteLine(
                Pierre.BackgroundKolor.Cyan,
                Pierre.ForegroundKolor.Yellow,
                "ColorsShouldBeBackToNormalAfterThis");
            Assert.AreEqual(Pierre.Konsole.DefaultFGColor, ConsoleColor.Green);
            Assert.AreEqual(Pierre.Kolor.ParseColor(Pierre.Kolor.ColorForeground), ConsoleColor.Green);
        }

        [DataTestMethod]
        [DataRow('0')]
        [DataRow('a')]
        [DataRow('A')]
        [DataRow('£')]
        [DataRow('$')]
        [DataRow('r')]
        public void IsReservedChar_IsTrue(char c)
        {
            Assert.IsTrue(Pierre.Kolor.IsReservedChar(c));
        }

        [DataTestMethod]
        [DataRow('&')]
        [DataRow('.')]
        [DataRow('-')]
        [DataRow('&')]
        [DataRow('*')]
        public void IsReservedChar_IsFalse(char c)
        {
            Assert.IsFalse(Pierre.Kolor.IsReservedChar(c));
        }
    }
}
