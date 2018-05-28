using System;

namespace AMKsGear.Core.Data.RawInterface.Triggers
{
    public class RawDbTrigger : IDbTrigger
    {
        public string Name { get; }
        public string Event { get; }
        public string Body { get; }

        public RawDbTrigger(string name, string @event, string body)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            if (body == null) throw new ArgumentNullException(nameof(body));
            Name = name;
            Event = @event;
            Body = body;
        }
    }
}