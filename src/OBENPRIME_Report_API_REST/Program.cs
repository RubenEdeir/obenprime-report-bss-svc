using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using OBENPRIME_Netsuite_API_REST.Utils;
using Service;
using System.Text;
using UnitOfWork_Interface;
using UnitOfWork_MySQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173", "https://obenprime-dev.com", "https://obenprime-qa.com", "https://obenprime.com", "https://ohgmiddleware.com", "http://172.168.20.3:8088", "http://172.168.20.3", "172.168.20.3", "https://webcmms.obenprime-dev.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWorkMySQL>();
builder.Services.AddTransient<INetsuiteService, NetsuiteService>();
builder.Services.AddTransient<MetodosExcel>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
builder.Services.AddApiVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = "https://srv-auth.obenprime-dev.com",
            ValidAudience = "https://srv-auth.obenprime-dev.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789qwertyuiopasdfghjklzxcvbnm2584kjh")),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.Use(async (contex, next) =>
{
    //string ambiente = "Production";
    string ambiente = "Development";
    var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
    if (config["ASPNETCORE_ENVIRONMENT"] == ambiente)
    {
        await next();
    }
    else
    {
        if (!contex.User.Identity?.IsAuthenticated ?? false)
        {
            contex.Response.StatusCode = 401;
            await contex.Response.WriteAsync("Usuario no autenticado");
        }
        else await next();
    }
});

app.MapControllers();

app.Run();
