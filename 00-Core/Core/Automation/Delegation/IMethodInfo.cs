using System;

namespace AMKsGear.Core.Automation.Delegation
{
    public interface IMethodInfo
    {
        string Name { get; }
        string Description { get; }
        Type ReturnType { get; }

        //IEnumerable<MethodParameter> Parameters { get; set; }
        MethodParameter[] GetParameters();

        IMethodInvoker MethodInvoker { get; }
    }
}