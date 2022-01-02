using OpenSleigh.Core.DependencyInjection;
using OpenSleigh.Persistence.Mongo;
using OpenSleigh.Transport.RabbitMQ;
using SagaCommon;
using ServiceTwo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddOpenSleigh(cfg =>
{
    cfg.UseRabbitMQTransport(new RabbitConfiguration("host.docker.internal", "guest", "guest"))
    .UseMongoPersistence(new MongoConfiguration("mongodb://user:user@host.docker.internal:27017/saga", "saga", MongoSagaStateRepositoryOptions.Default, MongoOutboxRepositoryOptions.Default));

    cfg.AddSaga<PaymentSaga, PaymentSagaState>()
    .UseStateFactory<BuySagaProcessWallet>(msg => new PaymentSagaState(msg.CorrelationId))
    .UseRabbitMQTransport()
    ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
