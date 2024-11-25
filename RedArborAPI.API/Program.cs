using RedArborAPI.Application.Validators;
using FluentValidation;
using RedArborAPI.Application.Commands;
using RedArborAPI.Infrastructure.Interfaces;
using RedArborAPI.Infrastructure.Services;
using System.Data;
using Microsoft.Data.SqlClient;
using RedArborAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using RedArborAPI.API.Converters;
using RedArborAPI.Application.Handlers;
using RedArborAPI.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configurar un formato personalizado de fechas
    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateEmployeeCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DeleteEmployeeCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateEmployeeCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEmployeesQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeeByIdQueryHandler>());
builder.Services.AddScoped<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteEmployeeCommand>, DeleteEmployeeCommandValidator>();
builder.Services.AddScoped<IValidator<GetEmployeeByIdQuery>, GetEmployeeByIdQueryValidator>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Connection")
    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


