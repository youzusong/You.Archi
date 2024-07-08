using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace You.Archi.Modularity
{
    /// <summary>
    /// 服务配置上下文
    /// </summary>
    public class ArcServiceConfigurationContext
    {
        public ArcServiceConfigurationContext([NotNull] IServiceCollection services)
        {
            this.Services = services;
            this.Items = new Dictionary<string, object?>();
        }

        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 附项集合
        /// </summary>
        public IDictionary<string, object?> Items { get; }

    }
}
