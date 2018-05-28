using System;
using System.Threading;

namespace AMKsGear.Core.Automation
{
    public interface IDisposeHandler
    {
        void DisposeObject(object disposeTarget);
    }

    public class DefaultDisposeHandler : IDisposeHandler
    {
        private static DefaultDisposeHandler DefaultDisposeHandlerInstance;

        public static DefaultDisposeHandler Instance
        {
            get
            {
                if (DefaultDisposeHandlerInstance != null) return DefaultDisposeHandlerInstance;

                LazyInitializer.EnsureInitialized(ref DefaultDisposeHandlerInstance);

                return DefaultDisposeHandlerInstance;
            }
        }


        public void DisposeObject(object disposeTarget)
        {
            (disposeTarget as IDisposable)?.Dispose();
        }
    }
}