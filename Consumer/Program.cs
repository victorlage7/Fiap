using Consumer;
using Consumer.Consumers;
using Consumer.Context;
using Consumer.Repositories;
using Consumer.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("PostgreSQL")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        //services.AddHostedService<Worker>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<IClienteService, ClienteService>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ClienteCriadoConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(context.Configuration["RabbitMQ:Host"], context.Configuration["RabbitMQ:VirtualHost"], h =>
                {
                    h.Username(context.Configuration["RabbitMQ:Username"]);
                    h.Password(context.Configuration["RabbitMQ:Password"]);
                });

                cfg.ReceiveEndpoint(context.Configuration["RabbitMQ:QueueName"], e =>
                {
                    e.ConfigureConsumer<ClienteCriadoConsumer>(ctx);
                });
            });
        });
    })
    .Build();

// Executar migrations automaticamente
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

host.Run();
