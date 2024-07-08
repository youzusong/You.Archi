namespace You.Archi.Modularity
{
    /// <summary>
    /// 依赖类型提供者接口
    /// </summary>
    public interface IArcDependedTypesProvider
    {
        /// <summary>
        /// 获取依赖类型集合
        /// </summary>
        /// <returns></returns>
        Type[] GetDependedTypes();
    }
}
