namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IYaModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        void ConfigureServices(YaServiceConfigurationContext context);

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <returns></returns>
        Task ConfigureServicesAsync(YaServiceConfigurationContext context);

    }
}
