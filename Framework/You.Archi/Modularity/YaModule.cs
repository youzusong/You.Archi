namespace You.Archi.Modularity
{
    public abstract class YaModule : IYaModule
    {
        public abstract void ConfigureServices(YaServiceConfigurationContext context);

        public abstract Task ConfigureServicesAsync(YaServiceConfigurationContext context);
    }
}
