# 💡 Exemplo de Integração com MassTransit e Azure Service Bus

Este projeto demonstra como utilizar o [MassTransit](https://masstransit.io/) com o **Azure Service Bus** em uma aplicação ASP.NET Core para envio e consumo de mensagens usando um modelo baseado em eventos.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8
- MassTransit
- Azure Service Bus
- Swagger (para testes via API)

---

## 📦 Funcionalidade

Este exemplo cobre:

- Criação de evento `PedidoCriado`
- Publicação do evento via `IPublishEndpoint`
- Consumo automático por `PedidoCriadoConsumer`
- Integração com Azure Service Bus para transporte de mensagens

---
