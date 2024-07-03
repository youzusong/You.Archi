using System.Diagnostics.CodeAnalysis;

namespace You.Archi.Application
{
    /// <summary>
    /// 应用程序关闭上下文
    /// </summary>
    public class YaApplicationShutdownContext
    {
        public YaApplicationShutdownContext([NotNull] IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}
