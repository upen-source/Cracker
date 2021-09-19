using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.MenuBuilder
{
    public class BoxBuilder
    {
        private const int AdditionalWidth = 5;

        public int    Width      { get; private set; }
        public string ExitOption { get; set; }

        public static int LongestWordOf(IEnumerable<string> words) =>
            words.Max(word => word.Length);

        private static void PrintHorizontalRule(int width)
        {
            Console.Write('+');
            Console.Write(new string('-', width));
            Console.WriteLine('+');
        }

        public static string VoidSpaceOf(int spaceWidth) => new('\0', spaceWidth);

        public void BoxIn(IEnumerable<string> elements)
        {
            IEnumerable<string> enumerable = elements.ToList();

            Width = LongestWordOf(enumerable);
            PrintHorizontalRule(Width + AdditionalWidth);
            enumerable.Select(DisplayElement).ToList().ForEach(Console.WriteLine);
            PrintHorizontalRule(Width + AdditionalWidth);
        }

        private string DisplayElement(string element, int index)
        {
            var elementIndex = $"{index + 1}.";
            if (element == ExitOption) elementIndex              = "0.";
            else if (string.IsNullOrEmpty(element)) elementIndex = "  ";

            var elementWithIndex = $"| {elementIndex} {element}";
            int spaceBetween     = Width - element.Length;
            var printableElement = $"{elementWithIndex}{VoidSpaceOf(spaceBetween)} |";

            return printableElement;
        }
    }
}
