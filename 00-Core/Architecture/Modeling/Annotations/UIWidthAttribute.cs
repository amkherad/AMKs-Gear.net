using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    public class UIWidthAttribute: Attribute
    {
        public int Width { get; set; }

        public UIWidthAttribute() { }
        public UIWidthAttribute(int width)
        {
            Width = width;
        }
    }
}