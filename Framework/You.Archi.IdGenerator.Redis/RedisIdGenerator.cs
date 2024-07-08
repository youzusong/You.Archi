using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Reflection;

namespace You.Archi.IdGenerator.Redis
{
    /// <summary>
    /// Redis之ID生成器
    /// <para>符号位（1bit） + 时间戳（31bit） + 序列ID(32bit)</para>
    /// </summary>
    public class RedisIdGenerator : IDisposable
    {
        private const long EPOCH_TIME = 63854236800L;   // 初始时间（秒）：2024-06-18
        private const byte SCQUENCE_BITS = 32;          // 序列ID位数

        private static readonly MethodInfo? _RedisConnectMethod;    // Redis连接方法

        //private readonly IDistributedCache _cache;
        private readonly RedisCache _cache;

        static RedisIdGenerator()
        {
            // 反射获取RedisCache的内部方法
            _RedisConnectMethod = typeof(RedisCache).GetMethod("Connect", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public RedisIdGenerator(IOptions<RedisCacheOptions> options)
        {
            _cache = new RedisCache(options);
        }

        /// <summary>
        /// 生成新ID
        /// </summary>
        /// <param name="sceneKey">业务场景Key</param>
        /// <returns>新ID</returns>
        public long NewId(string sceneKey)
        {
            // 获取时间戳(秒)
            var dtNow = DateTime.Now;
            var timestamp = dtNow.ToUniversalTime().Ticks / 10000000;

            // 获取序列ID
            var db = (IDatabase)_RedisConnectMethod?.Invoke(_cache, null)!;
            var key = $"incr:{sceneKey}:{dtNow.ToString("yyyyMMdd")}";
            var scquenceId = db.StringIncrement(key);

            // 拼接
            return ((timestamp - EPOCH_TIME) << SCQUENCE_BITS) | scquenceId;
        }

        public void Dispose()
        {
            _cache.Dispose();
        }

    }
}
