using System;
using System.Globalization;
using Presentation.Exceptions;
using Presentation.Filters;

namespace Presentation.Utils
{
    public static class ConsoleReader
    {
        [RepeatOnError]
        public static TNumeric ReadNumericData<TNumeric>(string question,
            Func<string, CultureInfo, TNumeric> parsing, ARange? range = null)
        {
            range ??= ARange.All;

            Console.Write(question);
            return AllowOnlyValidInput(parsing, range.Value);
        }

        private static TNumeric AllowOnlyValidInput<TNumeric>(
            Func<string, CultureInfo, TNumeric> parsing, ARange range)
        {
            try
            {
                TNumeric number = parsing(Console.ReadLine(), CultureInfo.InvariantCulture);
                if (range.HasValue(number as IComparable)) return number;
            }
            catch (FormatException e)
            {
                throw new InvalidUserActionException("Sólo ingrese números.", e);
            }

            throw new InvalidUserActionException("Número fuera de rango.");
        }
    }
}
