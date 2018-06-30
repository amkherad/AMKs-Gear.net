using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Framework;

[assembly: InternalsVisibleTo(FrameworkMembers.WebCoreId),
//           InternalsVisibleTo(FrameworkMembers.CoreTraceToolsId),
//
//           InternalsVisibleTo(FrameworkMembers.DataCoreId),
//
//           InternalsVisibleTo(FrameworkMembers.ServiceCoreId),
//
//           InternalsVisibleTo(FrameworkMembers.WebCoreId),
//           InternalsVisibleTo(FrameworkMembers.WebFancyPackId),
//
//           InternalsVisibleTo(FrameworkMembers.AppLayerCoreId),
//
//           InternalsVisibleTo(FrameworkMembers.DesktopCoreId),
//           InternalsVisibleTo(FrameworkMembers.WinCoreId),
//
//           InternalsVisibleTo(FrameworkMembers.NewtonsoftJsonId),
]

class AssemblyInfo
{
    public const string Id = FrameworkMembers.CoreId;
    public const string Name = FrameworkMembers.CoreName;
    public const string Guid = "cb15f3b2-c432-46d7-beaa-37afe9f81494";
}