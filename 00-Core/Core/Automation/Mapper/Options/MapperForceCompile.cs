namespace AMKsGear.Core.Automation.Mapper.Options
{
    public class MapperForceCompile : MapperOption
    {
        public const string OptionKey = "ForceCompile";
        
        public MapperForceCompile()
            : base(OptionKey, null)
        {
        }
    }
}