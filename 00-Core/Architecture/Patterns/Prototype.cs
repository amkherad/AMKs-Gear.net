using System;
using System.Collections.Generic;
//using AMKsGear.Architecture.Framework.Legacy;

namespace AMKsGear.Architecture.Patterns
{
    public interface IPrototype : ICloneable
    {

    }

    public static class PrototypePool
    {
        private static readonly List<IPrototype> Prototypes = new List<IPrototype>();

        public static void RegisterPrototype(IPrototype prototype)
        {
            if(prototype == null) throw new ArgumentNullException(nameof(prototype));
            Prototypes.Add(prototype);
        }

        public static TPrototype Clone<TPrototype>(TPrototype prototype)
            where TPrototype : IPrototype
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));

            var clone = prototype.Clone();
            if (!(clone is TPrototype)) throw new InvalidOperationException("Invalid Clone() result's type.");
            return (TPrototype) clone;
        }
    }
}