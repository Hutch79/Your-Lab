using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Your_Lab;

public class Setup
{
    private readonly WebApplicationBuilder _builder;


    public Setup(WebApplicationBuilder webApplicationBuilder)
    {
        _builder = webApplicationBuilder;
    }

    public void SetupRateLimiter()
    {
        var replenishmentPeriod = _builder.Configuration.GetValue<int>("YOUR_LAB:API:REPLENISHMENT_PERIOD", defaultValue: 600);
        var tokensPerPeriod = _builder.Configuration.GetValue<int>("YOUR_LAB:API:TOKENS_PER_PERIOD", defaultValue: 1);
        var tokenLimit = _builder.Configuration.GetValue<int>("YOUR_LAB:API:TOKEN_LIMIT", defaultValue: 100);
        var queueLimit = _builder.Configuration.GetValue<int>("YOUR_LAB:API:QUEUE_LIMIT", defaultValue: 0);
        _builder.Services.AddRateLimiter(_ => _
            .AddTokenBucketLimiter(policyName: "Token bucket", options =>
            {
                options.ReplenishmentPeriod = TimeSpan.FromMilliseconds(replenishmentPeriod);
                options.TokensPerPeriod = tokensPerPeriod;
                options.TokenLimit = tokenLimit;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = queueLimit;
            })
        );
    }
}