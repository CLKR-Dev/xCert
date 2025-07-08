namespace xCert.Settings;

public class PasswordsSettings: CommandSettings {
    [Description("Length of the password to generate")]
    [CommandOption("-l|--length")]
    [DefaultValue(32)]
    public int Length { get; set; }

    [Description("File name to save the generated password")]
    [CommandOption("-o|--out")]
    [DefaultValue("password.pwd")]
    public string FileName { get; set; }

    [Description("Print the generated password to console")]
    [CommandOption("-p|--print")]
    [DefaultValue(true)]
    public bool Print { get; set; }

    public override ValidationResult Validate() {
        if(Length < 1 || Length > 64)
            return ValidationResult.Error("Password length must be between 1 and 64 characters.");

        if(string.IsNullOrWhiteSpace(FileName))
            return ValidationResult.Error("File name cannot be empty.");

        return ValidationResult.Success();
    }
}
