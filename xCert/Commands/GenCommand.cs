using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace xCert.Commands;

internal class GenCommand : Command<GenSettings>
{
    public override int Execute(CommandContext context, GenSettings settings)
    {
        var (password, type) = PasswordsEngine.CheckPassword(settings.Password);

        AnsiConsole.MarkupLine($"[bold yellow]Generating certificate...[/]");
        AnsiConsole.MarkupLine($"[blue]> Name:[/] [green]{settings.Name}[/]");
        AnsiConsole.MarkupLine($"[blue]> Use case:[/] [italic dim]{settings.UseCase}[/]");
        AnsiConsole.MarkupLine($"[blue]> Password:[/] [italic dim]{type.ToString()}[/]");

        var (pwd, pfx, cer) = CertificatesEngine.GenerateCertificates(settings.Name, password, settings.UseCase);

        pwd.Write().To(settings.Name, $"{settings.Name}.pwd");
        pfx.Write().To(settings.Name, $"{settings.Name}.pfx");
        cer.Write().To(settings.Name, $"{settings.Name}.cer");

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold green]Certificate and private key successfully generated.[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.cer[/] [italic dim](public certificate)[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.pfx[/] [italic dim](with private key)[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.pwd[/] [italic dim](pwd for using pfx)[/]");

        return 0;
    }
}
