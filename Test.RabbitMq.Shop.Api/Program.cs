using Test.RabbitMq.Shop.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataDependencies();

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