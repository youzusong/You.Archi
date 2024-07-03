using Microsoft.Extensions.Options;

namespace You.Archi.IdGenerator.Memory
{
    /// <summary>
    /// 内存之ID生成器
    /// <para>符号位（1bit）+ 时间戳（41bit）+ 机房ID（5bit）+ 机器ID（5bit）+ 序号ID（12bit）</para>
    /// </summary>
    public class YaMemoryIdGenerator
    {
        private const long EPOCH_TIME = 63854236800000L;    // 初始时间（毫秒）：2024-06-18

        private const byte SCQUENCE_BITS = 12;      // 序列ID位数
        private const byte WORKERID_BITS = 5;       // 机器ID位数
        private const byte DATACENTERID_BITS = 5;   // 机房ID位数

        private const long SCQUENCE_MAX = -1L ^ (-1L << SCQUENCE_BITS);         // 序列ID最大值
        private const long WORKDERID_MAX = -1L ^ (-1L << WORKERID_BITS);        // 机器ID最大值
        private const long DATACENTERID_MAX = -1L ^ (-1L << DATACENTERID_BITS); // 机房ID最大值
        
        private const byte WORKERID_SHIFT = SCQUENCE_BITS;                              // 机器ID左移位数
        private const byte DATACENTERID_SHIFT = WORKERID_SHIFT + WORKERID_BITS;         // 机房ID左移位数
        private const byte TIMESTAMP_SHIFT = DATACENTERID_SHIFT + DATACENTERID_BITS;    // 时间戳左移位数

        private readonly object _lock = new();

        private readonly long _datacenterId = 0L;   // 机房ID
        private readonly long _workderId = 0L;      // 机器ID
        private long _sequenceId = 0L;              // 序号ID
        private long _lastTimestamp = 0L;           // 上次生成ID的时间戳

        public YaMemoryIdGenerator(IOptions<YaMemoryIdGeneratorOptions> options)
        {
            var datacenterId = options.Value.DatacenterId;
            var workerId = options.Value.WorkerId;

            if (datacenterId < 1)
                throw new ArgumentOutOfRangeException($"机房ID[{datacenterId}]小于1");

            if (datacenterId > DATACENTERID_MAX)
                throw new ArgumentOutOfRangeException($"机房ID[{datacenterId}]超出最大值[{DATACENTERID_MAX}]");

            if (workerId < 1)
                throw new ArgumentOutOfRangeException($"机器ID[{workerId}]小于1");

            if (workerId > WORKDERID_MAX)
                throw new ArgumentOutOfRangeException($"机器ID[{workerId}]超出最大值[{WORKDERID_MAX}]");

            _datacenterId = datacenterId;
            _workderId = workerId;
        }

        /// <summary>
        /// 生成新ID
        /// </summary>
        /// <returns>新ID</returns>
        public long NewId()
        {
            lock (_lock)
            {
                var timestamp = GetCurrTimestamp();
                if (timestamp < _lastTimestamp)
                    throw new Exception($"无法生成ID：当前时间戳[{timestamp}]小于上次时间戳[{_lastTimestamp}]");

                if (timestamp == _lastTimestamp)
                {
                    // 如果上次生成时间和当前生成时间在同一毫秒内，则序列ID自增
                    // 如果自增后的序列ID超出最大值，则等待到下一毫秒
                    _sequenceId = (_sequenceId + 1) & SCQUENCE_MAX;
                    if (_sequenceId == 0)
                    {
                        timestamp = GetNextTimestamp(_lastTimestamp);
                        _sequenceId = 1;
                    }
                }
                else
                {
                    // 序列ID归1
                    _sequenceId = 1;
                }

                // 记录生成时间
                _lastTimestamp = timestamp;

                // 拼接
                return ((timestamp - EPOCH_TIME) << TIMESTAMP_SHIFT) | (_datacenterId << DATACENTERID_SHIFT) | (_workderId << WORKERID_SHIFT) | _sequenceId;
            }
        }

        /// <summary>
        /// 获取当前时间戳（毫秒）
        /// </summary>
        /// <returns>当前时间戳（毫秒）</returns>
        private static long GetCurrTimestamp()
        {
            return DateTime.Now.ToUniversalTime().Ticks / 10000;
        }

        /// <summary>
        /// 获取下个时间戳（毫秒）
        /// </summary>
        /// <param name="lastTimestamp">上次时间戳</param>
        /// <returns>下个时间戳（毫秒）</returns>
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
