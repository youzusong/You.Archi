using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块加载器接口
    /// </summary>
    public interface IModuleLoader
    {
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">启动项模块类型</param>
        /// <returns>模块描述集合</returns>
        IModuleDescriptor[] LoadModules([NotNull] IServiceCollection services, [NotNull] Type startupModuleType);
    }
}
