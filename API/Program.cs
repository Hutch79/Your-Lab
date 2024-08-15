using Infrastructure;
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

    var dbHost = builder.Configuration.GetValue<string>("YOUR_LAB:DB:HOST");
    var dbPort = builder.Configuration.GetValue<int>("YOUR_LAB:DB:PORT");
    var dbDatabase = builder.Configuration.GetValue<string>("YOUR_LAB:DB:DATABASE");
    var dbUser = builder.Configuration.GetValue<string>("YOUR_LAB:DB:USER");
    var dbPassword = builder.Configuration.GetValue<string>("YOUR_LAB:DB:PASSWORD");

    connectionString = $"Host={dbHost}:{dbPort}; Database={dbDatabase}; Username={dbUser}; Password={dbPassword}";
    Console.WriteLine(connectionString);

    context.UseSqlServer(connectionString);
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
