namespace AMKsGear.Core.Automation.Mapper.Options
{
    public class MapperIgnoreValue : MapperOption
    {
        public const string OptionKey = "IgnoreValue";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        public MapperIgnoreValue(string propertyOrFieldName)
            : base(OptionKey, propertyOrFieldName)
        {
        }
    }
}