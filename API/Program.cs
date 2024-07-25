using System.Threading.RateLimiting;
using Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<YourLabDbContext>(context =>
{
    context.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    string connectionString = String.Empty;

    if (builder.Environment.IsProduction())
    {
        var dbHost = Environment.GetEnvironmentVariable("dbHost");
        var dbPort = Environment.GetEnvironmentVariable("dbPort");
        var dbName = Environment.GetEnvironmentVariable("dbName");
        var dbUser = Environment.GetEnvironmentVariable("dbUser");
        var dbPassword = Environment.GetEnvironmentVariable("dbPassword");

        connectionString = $"Server={dbHost}:{dbPort};Database={dbName};User ID={dbUser};Password={dbPassword};";
        Console.WriteLine(connectionString);

    }
    else if (builder.Environment.IsDevelopment())
    {
        connectionString = builder.Configuration.GetConnectionString("SqlConnectionString")!;
    }

    context.UseSqlServer(connectionString);
});


builder.Services.AddRateLimiter(_ => _ // Rate limiting to 100 requests per minute
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

app.UseHttpsRedirection();
app.UseRateLimiter();

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();