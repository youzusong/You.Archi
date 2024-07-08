using System.Diagnostics.CodeAnalysis;
using You.Archi.Application;

namespace You.Archi.Modularity
{
    /// <summary>
    /// 模块管理者接口
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        void InitializeModules([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <returns></returns>
        Task InitializeModulesAsync([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 关闭模块
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        void ShutdownModules([NotNull] ApplicationShutdownContext context);

        /// <summary>
        /// 关闭模块
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        /// <returns></returns>
        Task ShutdownModulesAsync([NotNull] ApplicationShutdownContext context);

    }
}
