namespace MassTransitApp.Eventos
{
    public class PedidoCriado
    {
        public Guid PedidoId { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
