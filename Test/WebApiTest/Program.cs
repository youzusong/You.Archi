using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using WebApiTest.Options;
using You.Archi.Json.NewtonsoftJson;
using You.Archi.Moment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Action<TestOptions> test = (opts) => {
    opts.EncryptSalt = "xxx";
};
//builder.Services.Configure<TestOptions>("", test);
//builder.Services.AddSingleton<IConfigureOptions<TestOptions>>(new ConfigureNamedOptions<TestOptions>("", test));

// Moment
builder.Services.AddOptions<MomentOptions>();
builder.Services.AddTransient<IMoment, Moment>();

// Mvc NewtonsoftJson
builder.Services.AddArchiMvcNewtonsoftJson(options => { });


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

app.Map("/config", (IConfiguration configuration, IOptions<NewtonsoftJsonOptions> opts) => {

    return opts.Value.SerializerSettings.DateFormatString;
   // return configuration["Security:EncryptKey"];
});

app.MapControllers();

app.Run();

