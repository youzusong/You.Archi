using Microsoft.Extensions.Options;

namespace You.Archi.IdGenerator.Memory
{
    public class YaMemoryIdGeneratorOptions : IOptions<YaMemoryIdGeneratorOptions>
    {
        public YaMemoryIdGeneratorOptions Value => this;

        public YaMemoryIdGeneratorOptions()
        {
            this.DatacenterId = 1;
            this.WorkerId = 1;
        }

        /// <summary>
        /// 机房ID
        /// </summary>
        public int DatacenterId { get; set; }

        /// <summary>
        /// 机器ID
        /// </summary>
        public int WorkerId { get; set; }

        
    }
}
