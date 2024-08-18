using Infrastructure;
using Infrastructure.DnsServices;
using Microsoft.EntityFrameworkCore;
using Your_Lab;

var builder = WebApplication.CreateBuilder(args);
var setup = new Setup(builder);

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
    var dbPort = builder.Configuration.GetValue<int>("YOUR_LAB:DB:PORT", defaultValue: 5432);
    var dbDatabase = builder.Configuration.GetValue<string>("YOUR_LAB:DB:DATABASE");
    var dbUser = builder.Configuration.GetValue<string>("YOUR_LAB:DB:USER");
    var dbPassword = builder.Configuration.GetValue<string>("YOUR_LAB:DB:PASSWORD");

    connectionString = $"Host={dbHost}:{dbPort}; Database={dbDatabase}; Username={dbUser}; Password={dbPassword}";
    context.UseNpgsql(connectionString);
});

setup.SetupRateLimiter();

var hetznerApiToken = builder.Configuration.GetValue<string>("YOUR_LAB:API_TOKEN:HETZNER_DNS");
var HetznerDns = new CloudflareDnsService(hetznerApiToken);
// var response = await HetznerDns.GetDnsRecords("wDU2E4E7pFZoJiFyGnt9E3");
var response = await HetznerDns.GetDnsRecords("2bbac90d6c425945fbbb8dd879abc30c"); // CF
// Console.WriteLine(response.Records.First());


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