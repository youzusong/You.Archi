using Microsoft.Extensions.Caching.StackExchangeRedis;
using You.Archi.IdGenerator.Memory;
using You.Archi.IdGenerator.Redis;

namespace IdGeneratorTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestRedisIdGenerator()
        {
            var options = new RedisCacheOptions
            {
                Configuration = "101.33.202.113:6379,password=Izayoi@1226"
            };

            var idGenerator = new ArcRedisIdGenerator(options);

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(idGenerator.NewId("order_create"));
            }
        }

        [TestMethod]
        public void TestMemoryIdGenerator()
        {
            var options = new ArcMemoryIdGeneratorOptions
            {
                DatacenterId = 1,
                WorkerId = 2
            };

            var idGenerator = new ArcMemoryIdGenerator(options);

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(idGenerator.NewId());
            }
        }
    }
}