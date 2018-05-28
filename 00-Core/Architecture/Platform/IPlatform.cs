using System;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Platform
{
    [Flags]
    public enum PlatformType
    {
        Unknown = 0,

        UserInteractivity = 0x10000000,

        Desktop = UserInteractivity | 0x100000,
        Mobile = UserInteractivity | 0x200000,
        Web = 0x400000,
        Service = 0x800000,

        #region Desktop
        WindowsFormsApplication = Desktop | 0x10001,
        WpfApplication = Desktop | 0x10002,

        JavaApplication = Desktop | 0x20001,
        #endregion
        #region Service
        WindowsService = Service | 0x10001,
        WebService = Service | 0x10001,
        #endregion
        #region Mobile

        #endregion
        #region Web
        WebFormsApplication = Web | 0x10001,
        MvcWebApplication = Web | 0x10002,

        WebApiApplication = Web | 0x10004,
        #endregion
    }

    /// <summary>
    /// A mechanism to provide basic platform specified information.
    /// </summary>
    public interface IPlatform : IWrapper
    {
        PlatformType Type { get; }
        string Name { get; }
        string Description { get; }

        string Host { get; }

        Type EntryType { get; }
        object EntryIfAvailable { get; }
    }
}