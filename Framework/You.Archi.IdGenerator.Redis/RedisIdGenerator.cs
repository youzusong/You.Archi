using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using System.Reflection;

namespace You.Archi.IdGenerator.Redis
{
    /// <summary>
    /// Redis之Id生成器
    /// </summary>
    public class RedisIdGenerator : IDisposable
    {
        private const long EPOCH_TIME = 63854236800L;   // 初始时间：2024-06-18
        private const int SCQUENCE_BIT = 32;            // 序列号长度

        private static readonly MethodInfo? _RedisConnectMethod;    // Redis连接方法
        private static readonly MethodInfo? _RedisDisposeMethod;    // Redis释放方法

        private readonly IDistributedCache _cache;

        static RedisIdGenerator()
        {
            // 反射获取RedisCache的内部方法
            var type = typeof(RedisCache);
            _RedisConnectMethod = type.GetMethod("Connect", BindingFlags.Instance | BindingFlags.NonPublic);
            _RedisDisposeMethod = type.GetMethod("Dispose", BindingFlags.Instance | BindingFlags.Public);
        }

        public RedisIdGenerator(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 生成新Id
        /// </summary>
        /// <param name="sceneKey">业务场景Key</param>
        /// <returns>新Id</returns>
        public long NewId(string sceneKey)
        {
            var dtNow = DateTime.Now;

            // 1、获取时间戳(秒)
            var timestamp = dtNow.ToUniversalTime().Ticks / 10000000 - EPOCH_TIME;

            // 2、获取序列号
            var db = (IDatabase)_RedisConnectMethod?.Invoke(_cache, null)!;
            var key = $"incr:{sceneKey}:{dtNow.ToString("yyyyMMdd")}";
            var scquence = db.StringIncrement(key);

            // 3、拼接：空位(1bit) + 时间戳(31bit) + 序列号(32bit)
            var result = timestamp << SCQUENCE_BIT | scquence;
            return result;
        }

        public void Dispose()
        {
            _RedisDisposeMethod?.Invoke(_cache, null);
        }

    }
}
