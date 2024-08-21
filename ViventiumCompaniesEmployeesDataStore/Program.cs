using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViventiumAPI.Data;
using ViventiumAPI.Controllers;
using ViventiumAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompaniesDATAContext>();
builder.Services.AddDbContext<EmployeesDATAContext>();
builder.Services.AddScoped<IDataStoreService, DataStoreService>();
builder.Services.AddScoped<ICompaniesService, CompaniesService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

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

app.Run();
