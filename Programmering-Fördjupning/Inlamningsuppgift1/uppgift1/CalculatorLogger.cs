using System;
using System.Collections.Generic;
using System.Text;

namespace uppgift1
{
    public class CalculatorLogger
    {
        readonly List<string> history = new List<string>();
        public CalculatorLogger()
        {
            history = new List<string>();
        }

        /// <summary>
        //  Formats the two strings into a string that looks like "{expression} => {result}" and adds it to the log.
        /// </summary>
        /// <param name="calculation"></param>
        /// <param name="result"></param>
        public void Log(string expression, string result) => history.Add($"{expression} => {result}");

        /// <summary>
        /// Clears log
        /// </summary>
        public void Clear() => history.Clear();

        /// <summary>
        /// returns a string of all logged messages
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (history.Count == 0)
                return "List is empty";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < history.Count; i++)
            {
                sb.Append(history[i]);
                if (i < history.Count - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
