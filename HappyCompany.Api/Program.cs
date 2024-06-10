using HappyCompany.Api.Endpoints.Warehouses;
using HappyCompany.App;
using HappyCompany.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HappyCompanyDbContext>();

builder.Services.RegisterAppModuleServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAppModuleMiddlwares();

app.MapWarehouseEndpoints();
app.Run();