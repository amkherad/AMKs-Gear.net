using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    public class VisibleAttribute : Attribute
    {
        public bool Visible { get; }

        public VisibleAttribute() { }
        public VisibleAttribute(bool visible)
        {
            Visible = visible;
        }
    }
}
