using MassTransit;
using MassTransitApp.Eventos;

namespace MassTransitApp.Consumers
{
    public class PedidoCriadoConsumer : IConsumer<Eventos.PedidoCriado>
    {
        public Task Consume(ConsumeContext<PedidoCriado> context)
        {
            var msg = context.Message;
            Console.WriteLine($"Pedido recebido: {msg.PedidoId} em {msg.CriadoEm}");
            return Task.CompletedTask;
        }
    }
}