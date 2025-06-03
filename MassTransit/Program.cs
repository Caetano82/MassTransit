using MassTransit;
using MassTransitApp.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
    
        cfg.Host("Endpoint=<ENDERECO DO BARRAMENTO, NAO DA FILA>");

        cfg.ReceiveEndpoint("input-queue", e =>
        {
            // all of these are optional!!

            e.PrefetchCount = 100;

            // number of "threads" to run concurrently
            e.MaxConcurrentCalls = 100;

            // default, but shown for example
            e.LockDuration = TimeSpan.FromMinutes(5);

            // lock will be renewed up to 30 minutes
            e.MaxAutoRenewDuration = TimeSpan.FromMinutes(30);
        });
        // Registrando consumidores
        cfg.ConfigureEndpoints(context);
    });

    // Registra o consumidor
    x.AddConsumer<PedidoCriadoConsumer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
