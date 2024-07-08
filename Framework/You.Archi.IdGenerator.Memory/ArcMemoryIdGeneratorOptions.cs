using Microsoft.Extensions.Options;

namespace You.Archi.IdGenerator.Memory
{
    public class ArcMemoryIdGeneratorOptions : IOptions<ArcMemoryIdGeneratorOptions>
    {
        public ArcMemoryIdGeneratorOptions Value => this;

        public ArcMemoryIdGeneratorOptions()
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
