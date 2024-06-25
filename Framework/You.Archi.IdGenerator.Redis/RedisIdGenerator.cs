namespace You.Archi.IdGenerator.Redis
{
    public class RedisIdGenerator
    {

        public long NewId(string keyPrefix)
        {
            var dtNow = DateTime.Now;

            // 获取时间戳
            var timestamp = 0L;


            // 获取序列号
            var key = $"incr:{keyPrefix}:{dtNow.ToString("yyyyMMdd")}";
            var count = 0L;

            // 拼接：空位(1bit) + 时间戳(31bit) + 序列号(32bit)
            var result = timestamp << 32 | count;
            return result;
        }

    }
}
