﻿using System.Diagnostics.CodeAnalysis;

namespace You.Archi.Application
{
    /// <summary>
    /// 应用程序初始化上下文
    /// </summary>
    public class YaApplicationInitializationContext
    {
        public YaApplicationInitializationContext([NotNull] IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}
