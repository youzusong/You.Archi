namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        public abstract void ConfigureServices(ServiceConfigurationContext context);

        public abstract Task ConfigureServicesAsync(ServiceConfigurationContext context);
    }
}
