using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
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
        private const byte SCQUENCE_BITS = 32;          // 序列号位数

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
        /// 生成新Id
        /// </summary>
        /// <param name="sceneKey">业务场景Key</param>
        /// <returns>新Id</returns>
        public long NewId(string sceneKey)
        {
            // 1、获取时间戳(秒)
            var dtNow = DateTime.Now;
            var timestamp = dtNow.ToUniversalTime().Ticks / 10000000 - EPOCH_TIME;

            // 2、获取序列号
            var db = (IDatabase)_RedisConnectMethod?.Invoke(_cache, null)!;
            var key = $"incr:{sceneKey}:{dtNow.ToString("yyyyMMdd")}";
            var scquence = db.StringIncrement(key);

            // 3、拼接：空位(1bit) + 时间戳(31bit) + 序列号(32bit)
            return (timestamp << SCQUENCE_BITS) | scquence;
        }

        public void Dispose()
        {
            _cache.Dispose();
        }

    }
}
