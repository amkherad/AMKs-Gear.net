using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AMKsGear.Architecture.Framework;

[assembly: InternalsVisibleTo(FrameworkMembers.CoreId),

           InternalsVisibleTo(FrameworkMembers.CoreTraceToolsId),
           InternalsVisibleTo(FrameworkMembers.DataCoreId),
//           InternalsVisibleTo(FrameworkMembers.OrmId),
//           InternalsVisibleTo(FrameworkMembers.RawSqlServerId),
//
//           InternalsVisibleTo(FrameworkMembers.ServiceCoreId),
//           InternalsVisibleTo(FrameworkMembers.ServiceWcfId),
//
           InternalsVisibleTo(FrameworkMembers.WebCoreId),
//           InternalsVisibleTo(FrameworkMembers.WebEngineId),
//           InternalsVisibleTo(FrameworkMembers.WebMvcId),
//           InternalsVisibleTo(FrameworkMembers.WebApiId),
//           InternalsVisibleTo(FrameworkMembers.WebFancyPackId),
//           InternalsVisibleTo(FrameworkMembers.WebFancyPackMvcId),
//
//           InternalsVisibleTo(FrameworkMembers.WebPresentationHelperId),
//
           InternalsVisibleTo(FrameworkMembers.AppLayerCoreId),
//           InternalsVisibleTo(FrameworkMembers.AppLayerServiceDataEfId),
//
//           InternalsVisibleTo(FrameworkMembers.DesktopCoreId),
//           InternalsVisibleTo(FrameworkMembers.WinCoreId),

//           InternalsVisibleTo(FrameworkMembers.NewtonsoftJsonId),
    
//           InternalsVisibleTo(FrameworkMembers.WebServicesId),
//           InternalsVisibleTo(FrameworkMembers.WinUxCoreId),
//           InternalsVisibleTo(FrameworkMembers.WinUxFormsId),
//           InternalsVisibleTo(FrameworkMembers.WpfCoreId),
//           InternalsVisibleTo(FrameworkMembers.PushingCoreId),
//           InternalsVisibleTo(FrameworkMembers.BusinessCoreId),
//           InternalsVisibleTo(FrameworkMembers.BusinessMvcId),
//           InternalsVisibleTo(FrameworkMembers.AmkCoreId),
//           InternalsVisibleTo(FrameworkMembers.AmkOrmId),
//           InternalsVisibleTo(FrameworkMembers.AmkWebId),
//           InternalsVisibleTo(FrameworkMembers.AmkWebHttpId),
//           InternalsVisibleTo(FrameworkMembers.AmkWebMvcId),
//           InternalsVisibleTo(FrameworkMembers.AmkWebMvcTemplateId),
//           InternalsVisibleTo(FrameworkMembers.AmkWebLibId),
]

[assembly: Guid("63963cc0-cec8-4de8-9dbf-d97c195a5db3")]

[assembly: AssemblyTitle(FrameworkMembers.ArchitectureName)] //FrameworkInfo.NameSpaced + FrameworkMembers.ArchitectureName + " by Ali Mousavi Kherad")]
[assembly: FrameworkMember(FrameworkMembers.ArchitectureId)]
[assembly: AssemblyDescription(FrameworkInfo.Description)]
[assembly: AssemblyCompany(FrameworkInfo.Company)]
[assembly: AssemblyProduct(FrameworkInfo.Product)]
[assembly: AssemblyCopyright(FrameworkInfo.Copyright)]
[assembly: AssemblyTrademark(FrameworkInfo.Trademark)]
[assembly: AssemblyVersion(FrameworkInfo.Version)]
[assembly: AssemblyFileVersion(FrameworkInfo.Version)]
[assembly: AssemblyInformationalVersion(FrameworkInfo.Version)]
//[assembly: ComVisible(FrameworkInfo.GeneralComVisibleState)]
[assembly: AssemblyCulture(FrameworkInfo.Calture)]