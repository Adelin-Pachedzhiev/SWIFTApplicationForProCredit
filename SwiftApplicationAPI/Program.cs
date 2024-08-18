using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SQLitePCL;
using SwiftApplicationAPI.Data;
using System;
using SwiftApplicationAPI;
using SwiftApplicationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//Initializing SQLite
Batteries.Init();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWebServices();
Log.Information("The application is starting");


var app = builder.Build();

//Creating Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SWIFTMessagesDataContext>();
    await context.Init();
}


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
