using System;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Web.Core.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspRequiredAttributeAttribute : Attribute
    {
        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }

        public string Attribute { get; private set; }
    }
}