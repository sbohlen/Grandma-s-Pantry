using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ConsoleInteraction
{
    /// <summary>
    /// Utility Class exposing methods to prompt for user input and return strongly-typed values.
    /// </summary>
    public static class InputRequester
    {
        /// <summary>
        /// Get user input as decimal type.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <returns>user input converted to decimal; 0 if value cannot be parsed from user input.</returns>
        public static decimal GetDecimalInput(string prompt)
        {
            ShowPrompt(prompt);
            string input = Console.ReadLine();

            decimal parsed;

            if (decimal.TryParse(input, out parsed))
                return parsed;
            else
                return 0;
        }

        /// <summary>
        /// Get user input as double type.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <returns>user input converted to double; 0 if value cannot be parsed from user input.</returns>
        public static double GetDoubleInput(string prompt)
        {
            ShowPrompt(prompt);
            string input = Console.ReadLine();

            double parsed;

            if (double.TryParse(input, out parsed))
                return parsed;
            else
                return 0;
        }

        /// <summary>
        /// Get user input as integer type.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <returns>user input converted to integer; 0 if value cannot be parsed from user input.</returns>
        public static int GetIntegerInput(string prompt)
        {
            ShowPrompt(prompt);
            string input = Console.ReadLine();

            int parsed;

            if (int.TryParse(input, out parsed))
                return parsed;
            else
                return 0;
        }

        /// <summary>
        /// Get user input as long type.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <returns>user input converted to long; 0 if value cannot be parsed from user input.</returns>
        public static long GetLongInput(string prompt)
        {
            ShowPrompt(prompt);
            string input = Console.ReadLine();

            long parsed;

            if (long.TryParse(input, out parsed))
                return parsed;
            else
                return 0;
        }

        /// <summary>
        /// Get user input as string type.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <returns>user input converted to string</returns>
        public static string GetStringInput(string prompt)
        {
            ShowPrompt(prompt);
            return Console.ReadLine();
        }

        private static void ShowPrompt(string prompt)
        {
            Console.Write("\n{0}: ", prompt);
        }

    }
}
