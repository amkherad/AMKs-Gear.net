using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Patterns.AppModel
{
    public delegate void StorageAppContextNamedValuesChanged(IStorageAppContext sender, string name, IEnumerable<object> values);
    public delegate void StorageAppContextValuesChanged(IStorageAppContext sender, Type type, IEnumerable<object> values);
}