using System.Reflection;

namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块描述接口
    /// </summary>
    public interface IModuleDescriptor
    {
        /// <summary>
        /// 模块实例
        /// </summary>
        IModule Instance { get; }

        /// <summary>
        /// 模块类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 模块程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 包含该模块的程序集集合
        /// </summary>
        Assembly[] AllAssemblies { get; }

        /// <summary>
        /// 是否作为插件载入
        /// </summary>
        bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 依赖模块的描述集合
        /// </summary>
        IReadOnlyList<IModuleDescriptor> Dependencies { get; }
    }
}
