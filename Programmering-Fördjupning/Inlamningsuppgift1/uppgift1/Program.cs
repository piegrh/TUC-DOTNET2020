using Pierre;
using System;
using System.Linq;
using System.Text;
namespace uppgift1
{
    public class Program
    {
        const float FahrenheitFreezingPoint = 32f;
        const string Commands = "Commands: ^^A^7bmi^^0 ^^A^7clear^^0 ^^A^7newton^^0 ^^A^7list^^0 ^^A^7quit^^0 ^^A^7help";
        static readonly CalculatorLogger logger = new CalculatorLogger();
        static void Main()
        {
            Konsole.WriteLine(Commands);
            while (true)
            {
                Console.Title = "Main";
                Console.WriteLine(Console.Title);
                string input = IOMaster.GetInput();
                if (string.IsNullOrWhiteSpace(input) || ExecuteCommand(input) || TemperatureConvertion(input))
                {
                    continue;
                }
                if (input == "quit")
                {
                    break;
                }
                if (IOMaster.IsNumericOrVariable(input))
                {
                    Calculate(input);
                    continue;
                }
                if (Kolor.IsColorString(input))
                {
                    Konsole.WriteLine(input);
                    continue;

                }
                IOMaster.PrintErrorMessage($"Unknown command! ({input})");
                Konsole.WriteLine(Commands);
            }
        }
        static bool ExecuteCommand(string input)
        {
            input = input.ToLower();
            switch (input)
            {
                case "help":
                    Konsole.WriteLine(Commands);
                    break;
                case "clear":
                    Console.Clear();
                    logger.Clear();
                    break;
                case "bmi":
                    Console.Title = "Enter height";
                    string height = IOMaster.GetNumericInput(errorMsg: "Invalid height", msg: "Enter height (cm)");
                    Console.Title = "Enter weight";
                    string weight = IOMaster.GetNumericInput(errorMsg: "Invalid weight", msg: "Enter weight (kg)");
                    float bmi = BMI(IOMaster.ParseToFloat(height), IOMaster.ParseToFloat(weight));
                    Console.WriteLine(bmi.ToString("0.0"));
                    logger.Log($"BMI({height},{weight})", bmi.ToString("0.0"));
                    break;
                case "list":
                    Konsole.WriteLine(ForegroundKolor.Magenta,logger.ToString());
                    break;
                case "newton":
                    Console.Title = "Enter m";
                    string m = IOMaster.GetNumericInput(errorMsg: "Invalid input. Try again.", msg: "m =");
                    Console.Title = "Enter a";
                    string a = IOMaster.GetNumericInput(errorMsg: "Invalid input. Try again.", msg: "a =");
                    float f = Newton(IOMaster.ParseToFloat(m), IOMaster.ParseToFloat(a));
                    logger.Log($"NEWTON({m},{a})", f.ToString("0.0"));
                    Console.WriteLine($"F = {f:0.0}");
                    break;
                default:
                    return false;
            }
            return true;
        }
        static bool TemperatureConvertion(string input)
        {
            if (IOMaster.IsNumberAndLastIsALetter(input))
            {
                string temperature = input.Substring(0, input.Length - 1);
                float convertedTemperature = 0;
                bool success = true;
                switch (input.Last())
                {
                    case 'f':
                        convertedTemperature = FahrenheitToCelsius(IOMaster.ParseToFloat(temperature));
                        Console.WriteLine($"{convertedTemperature}°C"); break;
                    case 'c':
                        convertedTemperature = CelsiusToFahrenheit(IOMaster.ParseToFloat(temperature));
                        Console.WriteLine($"{convertedTemperature}°F"); break;
                    default:
                        success = false;
                        break;
                }
                if (success)
                {
                    logger.Log(input, convertedTemperature.ToString());
                    return true;
                }
            }
            return false;
        }
        static void Calculate(string input)
        {
            StringBuilder sb = new StringBuilder();
            float result = IOMaster.ParseToFloat(input);
            sb.Append(input);
            while (true)
            {
                Console.Title = "Enter operator";
                string op = GetOperator();

                if (string.IsNullOrWhiteSpace(op))
                {
                    break;
                }

                Console.Title = "Enter number";
                string inputNumber = GetNumber();

                if (string.IsNullOrWhiteSpace(inputNumber))
                {
                    break;
                }

                float number = IOMaster.ParseToFloat(inputNumber);
                result = Compute(result, number, op[0]);
                sb.Append(op).Append(inputNumber);
                Console.WriteLine(result.ToString());
            }
            logger.Log(sb.ToString(), result.ToString());
        }
        private static string GetOperator()
        {
            string op;
            while (true)
            {
                op = IOMaster.GetInput();
                if (op.Length == 1 && IOMaster.IsOperator(op[0]) || string.IsNullOrWhiteSpace(op))
                {
                    break;
                }
                IOMaster.PrintErrorMessage($"Invalid operator ({Kolor.EscapeColors(op)})");
            }
            return op;
        }
        private static string GetNumber()
        {
            string inputNumber;
            while (true)
            {
                inputNumber = IOMaster.GetInput();
                if (!IOMaster.IsNumericOrVariable(inputNumber) && inputNumber.Length > 0)
                {
                    IOMaster.PrintErrorMessage($"Invalid number ({Kolor.EscapeColors(inputNumber)})");
                    continue;
                }
                break;
            }
            return inputNumber;
        }
        /// <summary>
        /// Computes the result of <paramref name="a"/> and <paramref name="b"/> with a given operator <paramref name="op"/> (+,-,*,/).
        /// </summary>
        /// <param name="a">A number</param>
        /// <param name="b">Another number</param>
        /// <param name="op">Operator</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <returns>a operator</returns>
        public static float Compute(float a, float b, char op)
        {
            switch (op)
            {
                case '+': return a += b;
                case '-': return a -= b;
                case '*': return a *= b;
                case '/': return a /= b;
                default:
                    throw new System.ArgumentException("Invalid operator.");
            }
        }
        /// <summary>
        /// Calculate bodymass index from <paramref name="hight"/> and <paramref name="weight"/>.
        /// </summary>
        /// <param name="hight">Hight in cm</param>
        /// <param name="weight">Weight in kg</param>
        /// <returns></returns>
        public static float BMI(float hight, float weight) => (weight / (hight * hight)) * 10_000f;
        /// <summary>
        /// Calculates the force of a body with a mass of <paramref name="m"/> and an acceleration of <paramref name="a"/>.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="a"></param>
        /// <returns>Force in Newtons</returns>
        public static float Newton(float m, float a) => m * a;
        /// <summary>
        /// Converts celsius to fahrenheit
        /// </summary>
        /// <param name="c">Temperature in celsius</param>
        /// <returns></returns>
        public static float CelsiusToFahrenheit(float c) => (c * 1.8f) + FahrenheitFreezingPoint;
        /// <summary>
        /// Converts fahrenheit to celsius
        /// </summary>
        /// <param name="f">Temperature in fahrenheit</param>
        /// <returns></returns>
        public static float FahrenheitToCelsius(float f) => (f - FahrenheitFreezingPoint) * 5f / 9f;
    }
}