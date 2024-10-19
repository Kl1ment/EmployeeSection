using EmployeeSection.API;
using EmployeeSection.Application;
using EmployeeSection.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services
    .AddRepositories(configuration)
    .AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.ApplyMigration();

app.Run();