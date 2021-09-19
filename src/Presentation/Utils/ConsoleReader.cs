using System;
using System.Globalization;
using Dawn;
using static System.Int32;

namespace Presentation.Utils
{
    public struct ARange
    {
        public        int    Start { get; }
        public        int    End   { get; }
        public static ARange All   => new(MinValue, MaxValue);

        public ARange(int start, int end)
        {
            Start = start;
            End   = end;
        }

        public bool In(IComparable value)
        {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }
    }

    public static class ConsoleReader
    {
        public static TNumeric ReadNumericData<TNumeric>(string question,
            Func<string, CultureInfo, TNumeric> parsing, ARange? range = null)
        {
            range ??= ARange.All;

            while (true)
            {
                Console.Write(question);
                try
                {
                    TNumeric number = parsing(Console.ReadLine(), CultureInfo.InvariantCulture);
                    if (range.Value.In(number as IComparable)) return number;
                    Console.Beep();
                    Console.WriteLine("Valor fuera de rango");
                }
                catch (FormatException)
                {
                    Console.Beep();
                    Console.WriteLine("Sólo ingrese valores númericos.");
                }
            }
        }
    }
}
