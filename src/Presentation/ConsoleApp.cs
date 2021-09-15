using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Logic;
using Microsoft.Extensions.Hosting;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly SomeService _service;

        public ConsoleApp(SomeService service)
        {
            _service = service;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Hello world");
            await _service.Save(new SomeEntity("SomeId", "Some Name"), cancellationToken);
            (await _service.GetAll(cancellationToken)).ToList().ForEach(Console.WriteLine);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
