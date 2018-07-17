using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Trace.UnitTesting.Annotations
{
    public class TestClassAttribute : OrderedAttribute
    {
        public bool Ignore { get; set; }
        public string Title { get; set; }
        
        public TestClassAttribute()
        {
        }
    }
}