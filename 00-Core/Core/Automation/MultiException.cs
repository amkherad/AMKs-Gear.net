using System;
using System.Linq;

namespace AMKsGear.Core.Automation
{
    public class MultiException : Exception
    {
        public Exception[] InnerExceptions { get; }

        public MultiException(Exception[] innerExceptions)
            : base(string.Join(Environment.NewLine, innerExceptions.Select(x => x.Message)))
        {
            InnerExceptions = innerExceptions;
        }
    }
}