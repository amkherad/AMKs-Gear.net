using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    public class CaptionAttribute : Attribute
    {
        public string Caption { get; }

        public CaptionAttribute(string caption)
        {
            Caption = caption;
        }
    }
}