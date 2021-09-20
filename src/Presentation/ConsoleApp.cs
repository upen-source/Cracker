using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Logic;
using Microsoft.Extensions.Hosting;
using Presentation.UIBuilder;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly SomeService _service;
        private readonly MenuBuilder _menuBuilder;

        public ConsoleApp(SomeService service, MenuBuilder menuBuilder)
        {
            _service     = service;
            _menuBuilder = menuBuilder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DisplayMenu(cancellationToken);
        }

        private async Task DisplayMenu(CancellationToken cancellationToken)
        {
            Menu menu = CreateMenuBuilder().Build();

            while (true)
            {
                await menu.DisplayAndReadAsync(cancellationToken);
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
                "Registrar nueva entidad", "Consultar todo", "Buscar", "Borrar", "Actualizar", "",
                "Salir"
            };
        }

        private IEnumerable<Func<CancellationToken, Task>> GetMenuActions()
        {
            return new Func<CancellationToken, Task>[]
            {
                RegisterNewEntity, ShowAll, FindOne, RemoveOne, UpdateOne, Menu.PassAsync,
                Menu.ExitAsync
            };
        }

        private static SomeEntity AskEntityData()
        {
            Console.Write("Ingrese un id: ");
            string id = Console.ReadLine();
            Console.Write("Ingrese un nombre: ");
            string name = Console.ReadLine();
            return new SomeEntity(id, name);
        }

        private async Task RegisterNewEntity(CancellationToken cancellationToken)
        {
            await _service.Add(AskEntityData(), cancellationToken);
        }

        private async Task ShowAll(CancellationToken cancellationToken)
        {
            (await _service.GetAll(cancellationToken)).ToList().ForEach(Console.WriteLine);
        }

        private async Task FindOne(CancellationToken cancellationToken)
        {
            Console.Write("Ingrese el id a buscar: ");
            string id = Console.ReadLine();
            Console.WriteLine($"Encontrado: {await _service.GetById(id, cancellationToken)}");
        }

        private async Task RemoveOne(CancellationToken cancellationToken)
        {
            Console.Write("Ingrese el id a borrar: ");
            string id = Console.ReadLine();
            await _service.RemoveById(id, cancellationToken);
            Console.WriteLine("Entidad borrada");
        }

        private async Task UpdateOne(CancellationToken cancellationToken)
        {
            Console.Write("Ingrese el id de la entidad para actualizar: ");
            string id = Console.ReadLine();
            Console.WriteLine("\nIngrese los nuevos datos.");
            await _service.UpdateById(id, AskEntityData(), cancellationToken);
            Console.WriteLine("Entidad actualizada.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
