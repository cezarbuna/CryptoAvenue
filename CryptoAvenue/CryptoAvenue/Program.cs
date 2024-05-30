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

builder.Services.AddHostedService<TimedCryptoUpdateService>();

builder.Services.AddDbContext<CryptoAvenueDbContext>(options =>
        options.UseSqlServer(builder.Configuration
        .GetConnectionString(@"Server = DESKTOP - DLVFJ7V\SQLEXPRESS; Database = CryptoAvenueDb; Trusted_Connection = True; TrustServerCertificate = True; MultipleActiveResultSets = True;")));

builder.Services.AddMediatR(typeof(CreateUserCommand));

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

app.MapControllers();

app.Run();
