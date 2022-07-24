using Microsoft.EntityFrameworkCore;
using Bank.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = $"Data source=bank.db";

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlite(connection));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
