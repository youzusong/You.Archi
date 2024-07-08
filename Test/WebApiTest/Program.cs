using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using WebApiTest.Options;
using You.Archi.Json.NewtonsoftJson;
using You.Archi.Json.NewtonsoftJson.Converter;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Action<TestOptions> test = (opts) => {
    opts.EncryptSalt = "xxx";
};
//builder.Services.Configure<TestOptions>("", test);
//builder.Services.AddSingleton<IConfigureOptions<TestOptions>>(new ConfigureNamedOptions<TestOptions>("", test));

// Mvc
var mvcBuilder = builder.Services.AddControllers();

// Mvc > Newtonsoft Json
//mvcBuilder.AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.DateFormatString = "yyyy年MM月dd日";
//    options.SerializerSettings.Converters.Add(new LongJsonConverter());
//    options.SerializerSettings.Converters.Add(new NullableLongJsonConverter());
//});

mvcBuilder.AddArchiNewtonsoftJson(options =>
{
    options.ArchiSerializerSettings.DateTimeFormat = "yyyy年MM月dd日 HH时mm分ss秒";
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

app.Map("/config", (IConfiguration configuration, IOptions<TestOptions> testOpts) => {
    return testOpts.Value.EncryptSalt;
   // return configuration["Security:EncryptKey"];
});

app.MapControllers();

app.Run();

