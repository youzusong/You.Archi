namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块容器接口
    /// </summary>
    public interface IArcModuleContainer
    {
        /// <summary>
        /// 模块描述集合
        /// </summary>
        IReadOnlyList<IArcModuleDescriptor> Modules { get; }
    }
}
