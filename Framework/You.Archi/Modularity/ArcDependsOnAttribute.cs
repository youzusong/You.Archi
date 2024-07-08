﻿namespace You.Archi.Modularity
{
    /// <summary>
    /// 依赖类型注册属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ArcDependsOnAttribute : Attribute, IArcDependedTypesProvider
    {
        public ArcDependsOnAttribute(params Type[] dependedTypes)
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