using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Presentation.UIBuilder;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly MenuBuilder _menuBuilder;

        public ConsoleApp(MenuBuilder menuBuilder)
        {
            _menuBuilder = menuBuilder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Menu menu = CreateMenuBuilder().Build();
            await DisplayMenu(menu, cancellationToken);
        }

        private static async Task DisplayMenu(Menu menu, CancellationToken cancellationToken)
        {
            while (true)
            {
                await menu.DisplayAndReadAsync(cancellationToken);
                Console.Write("\nPresione cualquier tecla para volver al menu...");
                Console.ReadKey();
            }
        }

        private MenuBuilder CreateMenuBuilder()
        {
            IEnumerable<string>                        options = GetMenuOptions();
            IEnumerable<Func<CancellationToken, Task>> actions = GetMenuActions();
            return _menuBuilder.WithOptions(options)
                .WithAsyncActions(actions)
                .WithExitOption("Salir")
                .WithClear(always: true)
                .WithQuestion("Ingrese una opcion: ");
        }

        private static IEnumerable<string> GetMenuOptions()
        {
            return new[]
            {
                "", "Salir"
            };
        }

        private IEnumerable<Func<CancellationToken, Task>> GetMenuActions()
        {
            return new Func<CancellationToken, Task>[]
            {
                Menu.PassAsync, Menu.ExitAsync
            };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
