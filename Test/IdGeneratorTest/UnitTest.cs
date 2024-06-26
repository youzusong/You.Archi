namespace IdGeneratorTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestRedisIdGenerator()
        {
            var max = 1024;
            var val = 1024;
            Console.WriteLine(val & max);

        }

        [TestMethod]
        public void TestMemoryIdGenerator()
        {

        }
    }
}