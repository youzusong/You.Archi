using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace You.Archi.Utilities
{
    /// <summary>
    /// 检验工具类
    /// </summary>
    [DebuggerStepThrough]
    public static class ArcCheckUtility
    {
        /// <summary>
        /// 不可为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramValue">参数值</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>参数值</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T NotNull<T>(T? paramValue, [NotNull] string paramName)
        {
            if (paramValue == null)
                throw new ArgumentNullException(paramName);

            return paramValue;
        }

        /// <summary>
        /// 不可为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramValue">参数值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">附加消息</param>
        /// <returns>参数值</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T NotNull<T>(T? paramValue, [NotNull] string paramName, string? message)
        {
            if (paramValue == null)
                throw new ArgumentNullException(paramName, message);

            return paramValue;
        }

        /// <summary>
        /// 必须大于0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramValue"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GreaterThanZero<T>(T paramValue, [NotNull] string paramName) where T : INumberBase<T>
        {
            if (T.IsNegative(paramValue) || T.IsZero(paramValue))
                throw new ArgumentException($"参数[{paramName}]必须大于0");
            else
                return paramValue;
        }
    }
}
