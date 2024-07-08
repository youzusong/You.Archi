namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IArcModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        void ConfigureServices(ArcServiceConfigurationContext context);

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <returns></returns>
        Task ConfigureServicesAsync(ArcServiceConfigurationContext context);

    }
}
