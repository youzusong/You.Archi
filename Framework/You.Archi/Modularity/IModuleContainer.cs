namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块容器接口
    /// </summary>
    public interface IModuleContainer
    {
        /// <summary>
        /// 模块描述集合
        /// </summary>
        IReadOnlyList<IModuleDescriptor> Modules { get; }
    }
}
