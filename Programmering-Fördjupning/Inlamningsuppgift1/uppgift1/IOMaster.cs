using System;
using Pierre;


namespace uppgift1
{
     /// <summary>
     /// Input/Output Master
     /// </summary>
    public static class IOMaster
    {
        const string Marcus = "MARCUS";
        const int MarcusValue = 42;
        /// <summary>
        /// Checks if c is an operator (+,-,*,/)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsOperator(char c)
        {
            switch (c)
            {
                case '+': return true;
                case '-': return true;
                case '*': return true;
                case '/': return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if text can be parsed into a float number
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Returns true if text is a number</returns>
        public static bool IsNumeric(string text) => float.TryParse(text, out float f) && !float.IsNaN(f);

        /// <summary>
        /// Checks if text is a variable name
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsVariable(string text) => text == Marcus;

        /// <summary>
        /// Checks if text is a number or a variable.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumericOrVariable(string text) => IsVariable(text) || IsNumeric(text);

        /// <summary>
        /// Checks if text is a number with a letter suffix
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumberAndLastIsALetter(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Length == 1)
                return false;
            return IsNumeric(text.Substring(0, text.Length - 1)) && char.IsLetter(text[text.Length - 1]);
        }
        /// <summary>
        /// Parses a string to a float.
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="OverflowException">The number was too big.</exception>
        /// <returns></returns>
        public static float ParseToFloat(string input)
        {
            if (IsVariable(input))
                return MarcusValue;
            try
            {
                return float.Parse(input);
            }
            catch (OverflowException)
            {
                // Bah! Just a little overflowexception, ignore it.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return float.NaN;
        }
        /// <summary>
        /// Prompts the user to enter some text.
        /// </summary>
        /// <param name="msg">Optional message to be shown before promting the user to enter something</param>
        /// <returns></returns>
        public static string GetInput(string msg = "")
        {
            PrintMessage(msg);
            Console.Write('>');
            string input = Console.ReadLine();
            return input.Trim();
        }
        /// <summary>
        /// Keeps prompting the user to enter a number/variable has been entered
        /// </summary>
        /// <param name="msg">Optional message to be shown before asking the user to enter something</param>
        /// <param name="errorMsg">Optinal message incase the user enters a invalid value</param>
        /// <returns>a string that is either a number or a variable</returns>
        public static string GetNumericInput(string msg = "", string errorMsg = "")
        {
            PrintMessage(msg);
            string input;
            while (true)
            {
                Console.Write('>');
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && IsNumericOrVariable(input))
                    break;
                PrintErrorMessage(errorMsg);
            }
            return input.Replace(" ", string.Empty);
        }
        public static void PrintMessage(string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
                Console.WriteLine(msg);
        }
        public static void PrintErrorMessage(string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
                Konsole.WriteLine(BackgroundKolor.Red, ForegroundKolor.Yellow, msg);
        }
    }
}
