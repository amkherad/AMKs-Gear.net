using AMKsGear.Architecture.Patterns;
using System.Collections.Generic;
using System;
using System.Collections;

namespace AMKsGear.Core.Patterns.AppModel
{
    public interface IStorageAppContext : IAppContext
    {
        event StorageAppContextNamedValuesChanged NamedValuesChanged;
        event StorageAppContextValuesChanged ValuesChanged;

        IEnumerable GetNamedValues();
        IEnumerable<object> GetValues(string name);
        IEnumerable<object> SetValues(string name, params object[] values);
        IEnumerable<object> SetValues(string name, IEnumerable<object> values);
        IEnumerable<object> AddValues(string name, IEnumerable<object> values);
        IEnumerable<object> RemoveValues(string name, IEnumerable<object> values);

        IEnumerable GetTypedValues();
        IEnumerable<T> GetValues<T>();
        IEnumerable<T> SetValues<T>(params T[] values);
        IEnumerable<T> SetValues<T>(IEnumerable<T> values);
        IEnumerable<T> AddValues<T>(IEnumerable<T> values);
        IEnumerable<T> RemoveValues<T>(IEnumerable<T> values);

        IEnumerable GetAllValues();
    }
}