using System;
using AMKsGear.Architecture.Automation;

namespace AMKsGear.Core.Automation.Mapper.Annotations
{
    public class MapperPathAttribute : Attribute
    {
        public IMemberPath Path { get; set; }
        
        public MapperPathAttribute()
        {
            
        }
    }
}