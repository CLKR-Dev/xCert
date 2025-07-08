namespace xCert.Commands;

internal class GenCommand : Command<GenSettings>
{
    public override int Execute(CommandContext context, GenSettings settings)
    {
        AnsiConsole.MarkupLine($"[bold yellow]Generating certificate...[/]");
        AnsiConsole.MarkupLine($"[blue]> Name:[/] [green]{settings.Name}[/]");
        AnsiConsole.MarkupLine($"[blue]> Use case:[/] [italic dim]{settings.UseCase}[/]");

        if (string.IsNullOrWhiteSpace(settings.Password))
            AnsiConsole.MarkupLine($"[blue]> Password:[/] [italic dim]Generated automatically[/]");
        else if (IOEngine.CheckAny(settings.Password))
            AnsiConsole.MarkupLine($"[blue]> Password:[/] [italic dim]Loaded from file[/]");
        else
            AnsiConsole.MarkupLine($"[blue]> Password:[/] [italic dim]Provided directly[/]");

        CertificatesEngine.GenerateCertificates(settings.Name, settings.Password, settings.UseCase);

        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine($"[bold green]Certificate and private key successfully generated.[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.cer[/] [italic dim](public certificate)[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.pfx[/] [italic dim](with private key)[/]");
        AnsiConsole.MarkupLine($"[blue]> {settings.Name}.pwd[/] [italic dim](password for using pfx)[/]");

        return 0;
    }
}
