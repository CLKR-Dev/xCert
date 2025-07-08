global using System.Reflection;
global using xCert.Core.Models;

using IODirectory = System.IO.Directory;

namespace xCert;

public static class GlobalContext {
    public static string Directory { get; private set; }
    public static Assembly Assembly { get; private set; }
    public static string AssemblyName { get; private set; }
    public static string AssemblyVersion { get; private set; }

    public static void Initialize() {
        Directory = IODirectory.GetCurrentDirectory();
        Assembly = Assembly.GetExecutingAssembly();
        AssemblyName = Assembly.GetName().Name!;
        AssemblyVersion = Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "Unknown";
    }
}
