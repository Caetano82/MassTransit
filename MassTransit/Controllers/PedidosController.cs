using MassTransit;
using MassTransitApp.Eventos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PedidosController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido()
        {
            var pedido = new PedidoCriado
            {
                PedidoId = Guid.NewGuid(),
                CriadoEm = DateTime.UtcNow
            };

            await _publishEndpoint.Publish(pedido);
            return Ok("Pedido enviado");
        }
    }
}