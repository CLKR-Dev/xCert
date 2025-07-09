global using System.ComponentModel;
global using Spectre.Console;
global using Spectre.Console.Cli;
global using xCert.Commands;
global using xCert.Settings;
global using xCert.Core.Engines;
global using xCert.Core.Models;

namespace xCert;

public class Program
{
    static void Main(string[] args)
    {
        GlobalContext.Initialize();
        
        var app = new CommandApp();
        app.Configure(Configure);
        app.Run(args);
    }

    static void Configure(IConfigurator config) {
        config.SetApplicationName(GlobalContext.AssemblyName);
        config.SetApplicationVersion(GlobalContext.AssemblyVersion);

        config.AddCommand<PasswordCommand>("password")
            .WithDescription("Generates a random password and saves it to a file or prints it to the console.")
            .WithExample("password", "--length", "32", "--out", "password.pwd", "--print")
            .WithExample("password", "--length", "16", "--print");

        config.AddCommand<GenCommand>("gen")
            .WithDescription("Generate a certificate (.pfx and .cer) based on use case")
            .WithExample([ "gen", "client", "MyClientCert", "MyPassword" ])
            .WithExample([ "gen", "server", "LocalhostCert", "SuperSecret" ]);
    }
}
