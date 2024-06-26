using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Redis Cache
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "101.33.202.113:6379,password=Izayoi@1226";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.Map("/redis", (IDistributedCache cache) =>
{
    cache.SetString("test", DateTime.Now.ToString());
    return "OK";
});

app.Run();

