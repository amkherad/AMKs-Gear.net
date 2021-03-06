﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Support
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}