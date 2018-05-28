using System.Collections.Generic;

namespace AMKsGear.Core.Automation.Delegation
{
    public interface IMethodInvoker
    {
        MethodCallResult Invoke(IMethodInfo methodInfo, IEnumerable<MethodParameter> parameters);
        MethodCallResult Invoke(IMethodInfo methodInfo, object[] parameters);
        MethodCallResult Invoke(IMethodInfo methodInfo, object[] inParameters, out object[] outParameters);
    }
}