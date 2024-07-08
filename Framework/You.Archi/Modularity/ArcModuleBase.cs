namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public abstract class ArcModuleBase : IArcModule
    {
        public abstract void ConfigureServices(ArcServiceConfigurationContext context);

        public abstract Task ConfigureServicesAsync(ArcServiceConfigurationContext context);
    }
}
