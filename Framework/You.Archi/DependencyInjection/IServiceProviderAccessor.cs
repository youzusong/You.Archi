namespace You.Archi.DependencyInjection
{
    /// <summary>
    /// 服务提供者之访问器接口
    /// </summary>
    public interface IServiceProviderAccessor
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
