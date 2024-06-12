using HappyCompany.Api.Endpoints.Users;
using HappyCompany.Api.Endpoints.Warehouses;
using HappyCompany.App;
using HappyCompany.Context;
using HappyCompany.JwtAuthentication;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HappyCompanyDbContext>();

builder.Services.RegisterAppModuleServices(builder.Configuration);
builder.Services.RegisterJWTAuthenticationModuleServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAppModuleMiddlwares();

app.MapWarehouseEndpoints();
app.MapUserEndpoints();

app.Run();
