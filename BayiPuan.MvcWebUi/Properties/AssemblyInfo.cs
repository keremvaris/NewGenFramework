using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NewGenFramework.Core.Aspects.Postsharp.ExceptionAspects;
using NewGenFramework.Core.Aspects.Postsharp.LogAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BayiPuan.MvcWebUi")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BayiPuan.MvcWebUi")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ExceptionLogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "BayiPuan.MvcWebUi.Controllers.*")]
[assembly: LogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "BayiPuan.MvcWebUi.Controllers.*")]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("07917f22-6f7a-4c86-be4a-57a04844ead3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
