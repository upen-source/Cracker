using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Logic;
using Microsoft.Extensions.Hosting;
using Presentation.MenuBuilder;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly SomeService             _service;
        private readonly MenuBuilder.MenuBuilder _menuBuilder;

        public ConsoleApp(SomeService service, MenuBuilder.MenuBuilder menuBuilder)
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
            string[] options =
            {
                "Usar todos los servicios", "Registrar nueva entidad", "Consultar todo", "Buscar",
                "", "Salir"
            };
            Func<CancellationToken, Task>[] actions =
            {
                UseAllServices, RegisterNewEntity, ShowAll, FindOne, Menu.PassAsync, Menu.ExitAsync
            };
            Menu menu = _menuBuilder
                .WithOptions(options)
                .WithAsyncActions(actions)
                .WithExitOption("Salir")
                .WithClear()
                .WithQuestion("Ingrese una opcion: ")
                .Build();

            while (true)
            {
                await menu.DisplayAndReadAsync(cancellationToken);
            }
        }

        private async Task UseAllServices(CancellationToken cancellationToken)
        {
            var ent  = new SomeEntity("123", "Some Name");
            var ent2 = new SomeEntity("Some Id", "Some Name");
            await _service.Add(ent, cancellationToken);
            await _service.Add(ent2, cancellationToken);

            SomeEntity found = await _service.GetById("123", cancellationToken);
            Console.WriteLine($"Found: {found}");

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
