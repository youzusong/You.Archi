using Microsoft.Extensions.Options;
using You.Archi.Json.NewtonsoftJson;
using You.Archi.Json.NewtonsoftJson.Converter;
using You.Archi.Moment;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static void AddArchiMvcNewtonsoftJson(this IServiceCollection services, Action<NewtonsoftJsonOptions> setupAction)
        {
            var mvcBuilder = services.AddControllers();

            var serviceProvider = mvcBuilder.Services.BuildServiceProvider().GetService<IServiceProvider>()!;
            var momentOptions = serviceProvider.GetRequiredService<IOptions<MomentOptions>>();

            services.Configure<NewtonsoftJsonOptions>(options =>
            {
                options.SerializerSettings.DateFormatString = momentOptions.Value.DateTimeFormat;
                options.SerializerSettings.Converters.Add(new LongJsonConverter());
                options.SerializerSettings.Converters.Add(new NullableLongJsonConverter());
                setupAction(options);
            });

            serviceProvider = mvcBuilder.Services.BuildServiceProvider().GetService<IServiceProvider>()!;
            var jsonOptions = serviceProvider.GetRequiredService<IOptions<NewtonsoftJsonOptions>>();

            mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = jsonOptions.Value.SerializerSettings.DateFormatString;
                options.SerializerSettings.Converters = jsonOptions.Value.SerializerSettings.Converters;
            });
        }
    }
}
