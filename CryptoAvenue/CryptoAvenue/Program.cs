
using CryptoAvenue.Application;
using CryptoAvenue.Application.Repositories;
using CryptoAvenue.Application.Services;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

builder.Services.AddHttpClient<ICoinGeckoApiService, CoinGeckoApiService>();

builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<ICryptoUpdateService, CryptoUpdateService>();

builder.Services.AddHostedService<TimedCryptoUpdateService>();

builder.Services.AddDbContext<CryptoAvenueDbContext>(options =>
        options.UseSqlServer(builder.Configuration
        .GetConnectionString(@"Server = DESKTOP - DLVFJ7V\SQLEXPRESS; Database = CryptoAvenueDb; Trusted_Connection = True; TrustServerCertificate = True; MultipleActiveResultSets = True;")));

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
