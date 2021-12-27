// See https://aka.ms/new-console-template for more information
using Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenSleigh.Core.DependencyInjection;
using OpenSleigh.Persistence.Mongo;
using OpenSleigh.Transport.RabbitMQ;
using Orchestrator;

Console.WriteLine("Start Orchestrator...");

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostCtx, services) =>
    {
        services.AddOpenSleigh(cfg =>
        {
            cfg.UseRabbitMQTransport(new RabbitConfiguration("localhost", "guest", "guest"))
            .UseMongoPersistence(new MongoConfiguration("mongodb://user:user@localhost:27017/saga", "saga", MongoSagaStateRepositoryOptions.Default, MongoOutboxRepositoryOptions.Default));

            cfg.AddSaga<BuySaga, BuySagaState>()
            .UseStateFactory<BuySagaStart>(msg => new BuySagaState(msg.CorrelationId))
            .UseRabbitMQTransport()
            ;
        });
        services.AddHttpClient();
        services.AddScoped<IServices, Services>();
    })
    .Build()
    .RunAsync();
