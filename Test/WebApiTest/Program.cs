using Microsoft.Extensions.Caching.Distributed;
using You.Archi.Json.NewtonsoftJson.Converter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Mvc
var mvcBuilder = builder.Services.AddControllers();

// Mvc > Newtonsoft Json
mvcBuilder.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyyÄêMMÔÂddÈÕ";
    options.SerializerSettings.Converters.Add(new YaLongJsonConverter());
    options.SerializerSettings.Converters.Add(new YaNullableLongJsonConverter());
});

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

app.Map("/config", (IConfiguration configuration) => {
    return configuration["Security:EncryptKey"];
});

app.MapControllers();

app.Run();

