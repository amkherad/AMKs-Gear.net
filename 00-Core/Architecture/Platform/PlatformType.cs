using System;

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
        WindowsApplication = Desktop | 0x10001,
        LinuxApplication = Desktop | 0x10002,
        #endregion
        #region Service
        WindowsService = Service | 0x10001,
        LinuxDaemon = Service | 0x10002,
        WebService = Service | 0x10005,
        #endregion
        #region Mobile
        WinPhoneApplication = Mobile | 0x10001,
        AndroidApplication = Mobile | 0x10002,
        IosApplication = Mobile | 0x10003,
        #endregion
        #region Web
        WebApplication = Web | 0x10001,
        WebApiApplication = Web | 0x10005,
        #endregion
    }
}