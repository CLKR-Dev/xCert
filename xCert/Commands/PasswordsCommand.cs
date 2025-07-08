namespace xCert.Commands;

public sealed class PasswordsCommand: Command<PasswordsSettings> {
    public override int Execute(CommandContext context, PasswordsSettings settings) {
        string password = PasswordsEngine.GeneratePassword(settings.Length);

        IOEngine.Write(password).To(settings.FileName);

        AnsiConsole.MarkupLine($"[green]Generated password saved to [/][bold underline]{settings.FileName}[/]");
        if(settings.Print)
            AnsiConsole.MarkupLine($"[green]Generated password: [/][bold]{PasswordsEngine.GeneratePassword(settings.Length)}[/]");

        return 0;
    }
}
