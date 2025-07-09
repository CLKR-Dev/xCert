namespace xCert.Commands;

public sealed class PasswordCommand: Command<PasswordSettings> {
    public override int Execute(CommandContext context, PasswordSettings settings) {
        string password = PasswordsEngine.GeneratePassword(settings.Length);

        IOEngine.Write(password).To(settings.FileName);

        AnsiConsole.MarkupLine($"[green]Generated password saved to [/][bold underline]{settings.FileName}[/]");
        if(settings.Print)
            AnsiConsole.MarkupLine($"[green]Generated password: [/][bold]{PasswordsEngine.GeneratePassword(settings.Length)}[/]");

        return 0;
    }
}
