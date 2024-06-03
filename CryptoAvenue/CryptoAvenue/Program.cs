using MediatR;
using CryptoAvenue.Application;
using CryptoAvenue.Application.Repositories;
using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.UserApp.UserCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CryptoAvenue.Dal.Repositories;
using CryptoAvenue.Application.WalletApp.WalletCommands;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = "temporary_key_to_be_replaced";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // Your Angular app's host and port
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddSingleton(new JwtTokenService(key));

builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

builder.Services.AddHttpClient<ICoinGeckoApiService, CoinGeckoApiService>();

builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ICryptoUpdateService, CryptoUpdateService>();
builder.Services.AddScoped<IWalletCoinRepository, WalletCoinRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddHostedService<TimedCryptoUpdateService>();

builder.Services.AddDbContext<CryptoAvenueDbContext>(options =>
        options.UseSqlServer(builder.Configuration
        .GetConnectionString(@"Server=DESKTOP-DLVFJ7V\SQLEXPRESS;Database=CryptoAvenueDb2;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Increase if necessary
    });


builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddMediatR(typeof(CreateWalletCommand));

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin"); // Use the CORS policy

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self' https://js.stripe.com; connect-src 'self' https://api.stripe.com https://q.stripe.com; frame-src 'self' https://js.stripe.com; style-src 'self' 'unsafe-inline';");
    await next();
});

app.MapControllers();

app.Run();
