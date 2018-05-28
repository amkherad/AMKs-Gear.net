using System;

namespace AMKsGear.Core.Automation.Object.Mapper.Options
{
    public class MappingDepth : MapperOption
    {
        public int Depth { get; }

        public MappingDepth(int depth)
        {
            if (depth <= 0) throw new ArgumentOutOfRangeException(nameof(depth));

            Depth = depth;
        }
    }
}