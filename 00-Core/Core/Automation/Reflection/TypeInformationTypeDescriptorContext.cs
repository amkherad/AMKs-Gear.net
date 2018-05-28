using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AMKsGear.Core.Automation.Reflection
{
    public class TypeInformationTypeDescriptorContext
    {
        public readonly string BindingStrongName;
        public readonly List<string> BindingNames = new List<string>();

        //public Type Binding { set { BindingStrongName = (value.FullName); } }

        public TypeInformationTypeDescriptorContext(string bindingStrongName)
        {
            BindingStrongName = bindingStrongName;
        }
        public TypeInformationTypeDescriptorContext(Type bindingType)
        {
            BindingStrongName = bindingType.FullName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual bool IsTypeEqualTo(TypeInformationTypeDescriptorContext descriptor)
        {
            if (descriptor.BindingStrongName == BindingStrongName) return true;
            var descriptorBindingNames = descriptor.BindingNames;
            if (descriptorBindingNames != null)
            {
                if (descriptorBindingNames.Any(x => x == BindingStrongName)) return true;
            }
            var bindingNames = BindingNames;
            if (bindingNames != null)
            {
                if (bindingNames.Any(x => x == descriptor.BindingStrongName)) return true;
            }

            if (bindingNames != null && descriptorBindingNames != null)
            {
                return descriptorBindingNames.Any(d => bindingNames.Any(b => b == d));
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual bool IsTypeEqualTo(string name) // => bindingStrongName == BindingStrongName
        {
            if (name == BindingStrongName) return true;
            var bindingNames = BindingNames;
            if (bindingNames != null)
            {
                return bindingNames.Any(x => x == name);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return BindingStrongName.GetHashCode();
        }
    }
}