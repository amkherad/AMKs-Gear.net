using System;

namespace AMKsGear.Architecture.Data.Serialization
{
    public interface ISerializer : IDisposable
    {
        void Serialize(object obj, params object[] options);
        object Deserialize(string source, Type type, params object[] options);
    }
}