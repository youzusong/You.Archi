namespace You.Archi.Modularity
{
    /// <summary>
    /// 依赖类型注册属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        public DependsOnAttribute(params Type[] dependedTypes)
        {
            this.DependedTypes = dependedTypes;
        }

        public Type[] DependedTypes { get; }

        public Type[] GetDependedTypes()
        {
            return this.DependedTypes;
        }
    }
}
