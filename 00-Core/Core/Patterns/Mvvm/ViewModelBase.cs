using System;
using System.Collections.Generic;
using AMKsGear.Core.Automation.LifetimeManagers;
using AMKsGear.Core.Automation.Support;

namespace AMKsGear.Core.Patterns.Mvvm
{
    public class ViewModelBase : NotifyPropertyChangedBase, IDisposable
    {
        protected virtual DisposableContainer DisposableContainer { get; }

        public ViewModelBase()
        {
            DisposableContainer = new DisposableContainer();
        }
        public ViewModelBase(IEnumerable<IDisposable> disposables)
        {
            DisposableContainer = new DisposableContainer(disposables);
        }

        public void Dispose() => DisposableContainer.Dispose();
    }
}