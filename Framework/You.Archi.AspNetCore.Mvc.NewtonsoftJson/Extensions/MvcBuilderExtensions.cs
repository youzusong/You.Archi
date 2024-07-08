using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using You.Archi.Json.NewtonsoftJson;
using You.Archi.Json.NewtonsoftJson.Converter;
using static System.Net.Mime.MediaTypeNames;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddArchiNewtonsoftJson(this IMvcBuilder builder, Action<ArcNewtonsoftJsonOptions> setupAction)
        {
            //var originSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            //builder.Services.AddSingleton<JsonSerializerSettings>(originSettings);

            //builder.Services.Configure(setupAction);

            //var serviceProvider = builder.Services.BuildServiceProvider().GetService<IServiceProvider>()!;
            //var arcOptions = serviceProvider.GetService<IOptions<ArcNewtonsoftJsonOptions>>()!;

            //builder.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.DateFormatString = arcOptions.Value.ArchiSerializerSettings.DateTimeFormat;
            //    options.SerializerSettings.Converters.Add(new ArcLongJsonConverter());
            //    options.SerializerSettings.Converters.Add(new ArcNullableLongJsonConverter());
            //});

            return builder;
        }
    }
}
