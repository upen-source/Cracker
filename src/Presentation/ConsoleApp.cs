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
                "Usar todos los servicios", "Registrar nueva entidad", "Consultar todo", "Buscar",
                "", "Salir"
            };
        }

        private IEnumerable<Func<CancellationToken, Task>> GetMenuActions()
        {
            return new Func<CancellationToken, Task>[]
            {
                UseAllServices, RegisterNewEntity, ShowAll, FindOne, Menu.PassAsync, Menu.ExitAsync
            };
        }

        private async Task UseAllServices(CancellationToken cancellationToken)
        {
            var ent  = new SomeEntity("123", "Some Name");
            var ent2 = new SomeEntity("Some Id", "Some Name");
            await _service.Add(ent, cancellationToken);
            await _service.Add(ent2, cancellationToken);

            SomeEntity found = await _service.GetById("123", cancellationToken);
            Console.WriteLine($"Encontrado: {found}");

            var updated = new SomeEntity("New Id", "New Name");
            await _service.UpdateById(ent2.Id, updated, cancellationToken);
            await _service.RemoveById("123", cancellationToken);

            await ShowAll(cancellationToken);
            await _service.RemoveById(updated.Id, cancellationToken);
        }

        private async Task RegisterNewEntity(CancellationToken cancellationToken)
        {
            Console.Write("Ingrese un id: ");
            string id = Console.ReadLine();
            Console.Write("Ingrese un nombre: ");
            string name = Console.ReadLine();
            await _service.Add(new SomeEntity(id, name), cancellationToken);
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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
