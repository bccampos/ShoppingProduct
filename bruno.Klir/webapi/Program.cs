using bruno.Klir.Application;
using bruno.Klir.Infrastructure;
using bruno.Klir.Contracts;
using bruno.Klir.WebApi;
using bruno.Klir.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebApi()
    .AddApplication()
    .AddDomain()
    .AddInfrastructure(builder.Configuration)
    .AddContracts();

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

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
