namespace xCert.Settings;

public class GenSettings: CommandSettings {
    [CommandArgument(0, "<UseCase>")]
    [Description("The type of certificate to generate (server, client, ca, codesigning, licensing)")]
    public string UseCaseRaw { get; set; }

    [CommandArgument(1, "<Name>")]
    [Description("Name of the certificate")]
    public string Name { get; set; }

    [CommandArgument(2, "[Password]")]
    [Description("Password for the generated .pfx file, path to file containing password or blank for auto-generated.")]
    [DefaultValue(null)]
    public string Password { get; set; }

    public CertificateUseCase UseCase { get; private  set; }

    public override ValidationResult Validate() {
        if(!Enum.TryParse<CertificateUseCase>(UseCaseRaw, ignoreCase: true, out var parsed)) {
            return ValidationResult.Error($"Invalid use case: '{UseCaseRaw}'");
        }

        UseCase = parsed;

        if(string.IsNullOrWhiteSpace(Name))
            return ValidationResult.Error("Name cannot be empty.");

        return ValidationResult.Success();
    }
}
