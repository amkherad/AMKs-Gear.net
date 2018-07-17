using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Trace.UnitTesting.Annotations
{
    public class TestMethodAttribute : OrderedAttribute
    {
        public bool Ignore { get; set; }
        public string Title { get; set; }
        
        
        
        public TestMethodAttribute()
        {
        }
    }
}