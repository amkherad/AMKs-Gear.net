using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public class ClassMemberInfo : IMemberInfo
    {
        public MethodInfo MethodInfo { get; }

        public ClassMemberInfo(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }
        public ClassMemberInfo(MemberInfo memberInfo)
        {
            //MethodInfo = methodInfo;
        }

        public object GetUnderlyingContext() => MethodInfo;

        public string Name => MethodInfo.Name;
    }
}