using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Utils;

namespace Presentation.MenuBuilder
{
    public class Menu
    {
        public string                                     Title        { get; set; }
        public string                                     Question     { get; set; }
        public IEnumerable<string>                        Options      { get; set; }
        public IEnumerable<Func<CancellationToken, Task>> AsyncActions { get; set; }
        public bool                                       ClearConsole { get; set; }
        public BoxBuilder                                 BoxBuilder   { get; set; }

        public Menu(BoxBuilder boxBuilder)
        {
            BoxBuilder   = boxBuilder;
            AsyncActions = new List<Func<CancellationToken, Task>>();
            Options      = new List<string>();
        }

        public string ExitOption
        {
            set => BoxBuilder.ExitOption = value;
        }

        public void AddAsyncOption(string option, Func<CancellationToken, Task> action)
        {
            List<string> updatedOptions = Options.ToList();
            updatedOptions.Add(option);
            List<Func<CancellationToken, Task>> updatedActions = AsyncActions.ToList();
            updatedActions.Add(action);

            Options      = updatedOptions;
            AsyncActions = updatedActions;
        }

        private void Display()
        {
            if (ClearConsole) Console.Clear();
            DisplayTitle();
            BoxBuilder.BoxIn(Options);
        }

        public async Task DisplayAndReadAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await ExitAsync(cancellationToken);
            }

            Display();
            int choice = ConsoleReader.ReadNumericData(Question, Convert.ToInt32,
                new ARange(0, AsyncActions.Count() - 2));
            await ExecuteOptionAsync(choice, cancellationToken);
            Console.Write("Presione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }


        private void DisplayTitle()
        {
            int    menuWidth  = BoxBuilder.LongestWordOf(Options) + 8;
            int    space      = Math.Max((menuWidth - Title.Length) / 2, 0);
            string titleSpace = BoxBuilder.VoidSpaceOf(space);
            Console.WriteLine($"{titleSpace}{Title}\n");
        }

        private async Task ExecuteOptionAsync(int choice, CancellationToken cancellationToken)
        {
            await AsyncActions.ElementAt(GetMenuIndex(choice))(cancellationToken);
        }

        private int GetMenuIndex(int choice)
        {
            const int exit            = 0;
            int       lastOptionIndex = Options.Count();
            int       menuIndex       = (choice == exit ? lastOptionIndex : choice) - 1;
            return menuIndex;
        }

        public static Task ExitAsync(CancellationToken cancellationToken)
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }

        public static Task PassAsync(CancellationToken cancellationToken)
        {
            // Do nothing in a menu
            return Task.CompletedTask;
        }
    }
}
