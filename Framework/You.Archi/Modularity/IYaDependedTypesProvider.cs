namespace You.Archi.Modularity
{
    /// <summary>
    /// 依赖类型提供者接口
    /// </summary>
    public interface IYaDependedTypesProvider
    {
        Type[] GetDependedTypes();
    }
}
