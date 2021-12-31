using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenSleigh.Core.DependencyInjection;
using OpenSleigh.Core.Messaging;
using OpenSleigh.Persistence.Mongo;
using OpenSleigh.Transport.RabbitMQ;
using SagaCommon;

Console.WriteLine("Start console client...");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostCtx, services) =>
    {
        services.AddOpenSleigh(cfg =>
        {
            cfg.SetPublishOnly()
                .UseRabbitMQTransport(new RabbitConfiguration("host.docker.internal", "guest", "guest"))
                .UseMongoPersistence(new MongoConfiguration("mongodb://user:user@host.docker.internal:27017/saga", "saga", MongoSagaStateRepositoryOptions.Default, MongoOutboxRepositoryOptions.Default));
        });
    })
    .Build();

SendStartingMessage(host.Services);

await host.RunAsync();


static async Task SendStartingMessage(IServiceProvider serviceProvider)
{
    var id = Guid.NewGuid();
    var cid = Guid.NewGuid();
    var msg = new BuySagaStart(id, cid);

    Console.WriteLine($"Send msg with Id: {id} - CorrelationId {cid}");

    var bus = serviceProvider.GetRequiredService<IMessageBus>();
    await bus.PublishAsync(msg);
}