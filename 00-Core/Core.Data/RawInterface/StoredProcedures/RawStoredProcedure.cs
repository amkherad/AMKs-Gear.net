using System;

namespace AMKsGear.Core.Data.RawInterface.StoredProcedures
{
    public class RawStoredProcedure : IStoredProcedure
    {
        public string Name { get; }
        public string Body { get; }

        public RawStoredProcedure(string name, string body)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (body == null) throw new ArgumentNullException(nameof(body));
            Name = name;
            Body = body;
        }
    }
}