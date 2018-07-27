using System;
using System.Linq.Expressions;

namespace AMKsGear.Architecture.Automation
{
    public class MemberPath<TRoot> : IMemberPath
    {
        public Expression<Func<TRoot, object>> Path { get; }
        Expression IMemberPath.Path => Path;

        public MemberPath(Expression<Func<TRoot, object>> path)
        {
            Path = path;
        }
    }
}