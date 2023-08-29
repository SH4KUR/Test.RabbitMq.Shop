using MassTransit;
using Test.RabbitMq.Shop.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataLayerDependencies();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.UseMessageRetry(r => r.Interval(3, 1000));
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(b =>b.AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();

SeedData();

app.Run();

void SeedData()
{
    using var scope = app.Services.CreateScope();
    
    var dbInitializer = scope.ServiceProvider.GetRequiredService<TestDataSeeder>();
    dbInitializer.SeedData();
}