using System;

namespace AMKsGear.Core.Data.Annotations
{
    public abstract class TriggerAttribute : Attribute
    {
        public string TriggerName { get; set; }
        public CrudActions Event { get; set; }

        public TriggerAttribute(CrudActions @event)
        {
            Event = @event;
        }
    }
}