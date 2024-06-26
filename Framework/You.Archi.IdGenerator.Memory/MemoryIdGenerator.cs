using Microsoft.Extensions.Options;

namespace You.Archi.IdGenerator.Memory
{
    public class MemoryIdGenerator
    {
        private const long EPOCH_TIME = 63854236800000L;    // 初始时间：2024-06-18

        private const byte WORKERID_BITS = 5;   // 工作机器id位数
        private const byte SCENEID_BITS = 5;    // 业务场景id位数
        private const byte SCQUENCE_BITS = 12;  // 序列id位数

        private const byte SCENEID_SHIFT = SCQUENCE_BITS;                   // 业务场景id左移位数
        private const byte WORKERID_SHIFT = SCENEID_SHIFT + WORKERID_BITS;  // 工作机器id左移位数
        private const byte TIMESTAMP_SHIFT = WORKERID_SHIFT + SCENEID_BITS; // 时间戳左移位数

        private const long WORKDERID_MAX = (1 << WORKERID_BITS) - 1;    // 工作机器id最大值
        private const long SCENEID_MAX = (1 << SCENEID_BITS) - 1;       // 业务场景id最大值
        private const long SCQUENCE_MAX = (1 << SCQUENCE_BITS) - 1;     // 序列id最大值

        private static readonly IDictionary<string, long> _SceneIdCache = new Dictionary<string, long>();       // 场景id缓存
        private static readonly IDictionary<string, long> _ScquenceIdCache = new Dictionary<string, long>();    // 序列id缓存

        //private readonly object _lock = new Object();

        private readonly long _datacenterId = 0L;
        private readonly long _workerId = 0L;
        private long _lastTimestamp = -1L;

        public MemoryIdGenerator(IOptions<MemoryIdGeneratorOptions> options)
        {
            var workderId = options.Value.WorkerId;

            if (workderId > WORKDERID_MAX)
                throw new Exception($"工作机器id[{workderId}]超出最大值[{WORKDERID_MAX}]");

            _workerId = workderId;
        }

        public long NewId(string sceneKey)
        {
            if (String.IsNullOrEmpty(sceneKey))
                throw new ArgumentNullException(nameof(sceneKey));

            lock (String.Intern(sceneKey))
            {
                var _scenceId = 0L;
                var _scquenceId = 0L;

                if (_SceneIdCache.ContainsKey(sceneKey))
                {
                    _scenceId = _SceneIdCache[sceneKey];
                }
                else
                {
                    _scenceId = _SceneIdCache.Count + 1;
                    if (_scquenceId > SCENEID_MAX)
                        throw new Exception($"业务场景id[{_scenceId}]超出最大值[{SCENEID_MAX}]");

                    _SceneIdCache.Add(sceneKey, _scenceId);
                }

                if (_ScquenceIdCache.ContainsKey(sceneKey))
                {
                    _scquenceId = _ScquenceIdCache[sceneKey];
                }
                else
                {
                    _scquenceId = 1;
                    _ScquenceIdCache.Add(sceneKey, _scquenceId);
                }

                // 获取当前时间戳
                var timestamp = GetCurrTimestamp();

                // 如果当前时间戳小于上次时间戳，则抛出异常
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception($"当前时间戳[{timestamp}]小于上次时间戳[{_lastTimestamp}]");
                }

                if (timestamp == _lastTimestamp)
                {
                    // 如果在同一毫秒内产生Id，序列id自增，如果超出最大值，则等待到下一毫秒
                    var scquenceId = _ScquenceIdCache[sceneKey];
                    scquenceId = (scquenceId + 1) & SCQUENCE_MAX;
                    if (scquenceId == 0)
                    {
                        timestamp = GetNextTimestamp(_lastTimestamp);
                    }
                }
                else
                {
                    // 序列号归1
                    _scenceId = 1;
                    _ScquenceIdCache[sceneKey] = 1;
                }

                // 符号位(1bit) + 时间戳(41bit) + 工作机器id(5bit) + 业务场景id(5bit) + 序列id(10bit)
                return (timestamp << TIMESTAMP_SHIFT) | (_workerId << WORKERID_SHIFT) | (_scenceId << SCENEID_SHIFT) | _scenceId;
            }
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns>当前时间戳</returns>
        private static long GetCurrTimestamp()
        {
            return DateTime.Now.ToUniversalTime().Ticks / 10000 - EPOCH_TIME;
        }

        /// <summary>
        /// 获取下个时间戳
        /// </summary>
        /// <param name="lastTimestamp">上次时间戳</param>
        /// <returns>下个时间戳</returns>
        private static long GetNextTimestamp(long lastTimestamp)
        {
            var currTimestamp = GetCurrTimestamp();

            while (currTimestamp <= lastTimestamp)
            {
                currTimestamp = GetCurrTimestamp();
            }

            return currTimestamp;
        }

    }
}
