using System;
using System.Collections.Generic;
using AMKsGear.Core.Automation;
using AMKsGear.Core.Automation.Support;

namespace AMKsGear.Core.Patterns.Mvvm
{
    public class ViewModelBase : NotifyPropertyChangedBase, IDisposable
    {
        protected virtual Disposer Disposer { get; }

        public ViewModelBase()
        {
            Disposer = new Disposer();
        }
        public ViewModelBase(IEnumerable<IDisposable> disposables)
        {
            Disposer = new Disposer(disposables);
        }

        public void Dispose() => Disposer.Dispose();
    }
}