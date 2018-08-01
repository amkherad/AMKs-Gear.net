using System;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Automation.Mapper.Annotations
{
    public class MapperPathAttribute : Attribute
    {
        public IModelMemberPath Path { get; set; }
        
        public MapperPathAttribute()
        {
            
        }
    }
}