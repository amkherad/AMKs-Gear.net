namespace AMKsGear.Core.Automation
{
    public class TagExtender : TagExtender<object, object>
    {
        public TagExtender() { }
        public TagExtender(object value)
        {
            Value = value;
        }
        public TagExtender(object value, object tag)
        {
            Value = value;
            Tag = tag;
        }
    }
    public class TagExtender<TValue> : TagExtender<TValue, object>
    {
        public TagExtender() { }
        public TagExtender(TValue value)
        {
            Value = value;
        }
        public TagExtender(TValue value, object tag)
        {
            Value = value;
            Tag = tag;
        }
    }
    public class TagExtender<TValue, TTag>
    {
        public TagExtender() { }
        public TagExtender(TValue value)
        {
            Value = value;
        }
        public TagExtender(TValue value, TTag tag)
        {
            Value = value;
            Tag = tag;
        }

        public TValue Value { get; set; }
        public TTag Tag { get; set; }
    }
}
