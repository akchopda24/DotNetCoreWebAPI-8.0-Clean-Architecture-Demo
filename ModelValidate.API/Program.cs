using Microsoft.EntityFrameworkCore;
using ModelValidate.BusinessLogic.Interface;
using ModelValidate.BusinessLogic.Service;
using ModelValidate.DataAccess.Entities;
using ModelValidate.DataAccess.GenericRepository;
using ModelValidate.DataAccess.Profiler;
using System;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register DI service here
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IValidateMessageService, ValidateMessageService>();

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Add cors policy
const string policy = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// here we used InMemory DB for validate message testing
builder.Services.AddDbContext<ValidateMessageDbContext>(options =>
        options.UseInMemoryDatabase("ValidateMessageDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy);

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

//Open Web Page in new browser to test input message as validate or not.
System.Diagnostics.Process.Start("cmd", "/C start https://localhost:7224");

app.Run();

