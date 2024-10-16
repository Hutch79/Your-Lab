using System.Threading.RateLimiting;
using Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<YourLabDbContext>(context =>
{
    var dbHost = Environment.GetEnvironmentVariable("DB__Host");  // TODO: Connection string builder
    var dbPort = Environment.GetEnvironmentVariable("DB__Port");
    var dbDatabase = Environment.GetEnvironmentVariable("DB__Database");
    var dbUser = Environment.GetEnvironmentVariable("DB__User");
    var dbPassword = Environment.GetEnvironmentVariable("DB__Password");


    SqlConnectionStringBuilder hui = new(); // TODO: USE IT!!!! Aber für Postgres guggen obs funzt
    hui.Password = dbPassword;

    context.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    var connectionString = $"Host={dbHost}:{dbPort}; Database={dbDatabase}; Username={dbUser}; Password={dbPassword}";
    context.UseNpgsql(connectionString);
});

// ToDo: Rate limit options as env variables
builder.Services.AddRateLimiter(_ => _  // Rate limiting to 100 requests per minute
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 25;
        options.Window = TimeSpan.FromSeconds(15);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<YourLabDbContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();
app.UseHttpsRedirection();

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
