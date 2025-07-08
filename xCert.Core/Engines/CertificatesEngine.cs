using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace xCert.Core.Engines;

public static class CertificatesEngine {
    public static void GenerateCertificates(string name, string passwordPath, CertificateUseCase useCase) {
        string password = string.Empty;

        if(string.IsNullOrEmpty(passwordPath)) {
            // Generate a random password if none is provided
            password = PasswordsEngine.GeneratePassword(32);
        }
        else if(IOEngine.CheckAny(passwordPath)) {
            // Read password from file if it exists
            password = IOEngine.ReadAny(passwordPath);
        }
        else {
            // Use the provided password path as the password
            password = passwordPath;
        }

        // Create a new RSA key pair for the certificate
        using var rsa = RSA.Create(2048);

        // Create a certificate request with the specified name and RSA key
        var request = new CertificateRequest($"CN={name}", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        // Apply the appropriate extensions based on the use case
        ApplyExtensions(request, useCase);

        // Create the self-signed certificate with a validity period of 5 years
        var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(5));

        // Export the password, certificate and private key to files
        password.Write().To(name, $"{name}.pwd");
        cert.Export(X509ContentType.Pfx, password).Write().To(name, $"{name}.pfx");
        cert.Export(X509ContentType.Cert).Write().To(name, $"{name}.cer");
    }

    // Apply the necessary extensions to the certificate request based on the use case
    private static void ApplyExtensions(CertificateRequest request, CertificateUseCase useCase) {
        request.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(request.PublicKey, false));

        switch(useCase) {
            case CertificateUseCase.Server:
                ApplyServerExtensions(request);
                break;
            case CertificateUseCase.Client:
                ApplyClientExtensions(request);
                break;
            case CertificateUseCase.CA:
                ApplyCAExtensions(request);
                break;
            case CertificateUseCase.CodeSigning:
                ApplyCodeSigningExtensions(request);
                break;
            case CertificateUseCase.Licensing:
                ApplyLicensingExtensions(request);
                break;
        }
    }

    // Server extensions are used for server authentication
    private static void ApplyServerExtensions(CertificateRequest request) {
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
            X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment, true));
        request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
            new OidCollection {
                new Oid("1.3.6.1.5.5.7.3.1") // Server Auth
            }, true));
    }

    // Client extensions are used for client authentication
    private static void ApplyClientExtensions(CertificateRequest request) {
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
            X509KeyUsageFlags.DigitalSignature, true));
        request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
            new OidCollection {
                new Oid("1.3.6.1.5.5.7.3.2") // Client Auth
            }, true));
    }

    // CA (Certificate Authority) extensions are used for issuing certificates
    private static void ApplyCAExtensions(CertificateRequest request) {
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
            X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, true));
    }

    // Code Signing extensions are typically used for signing applications and scripts
    private static void ApplyCodeSigningExtensions(CertificateRequest request) {
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
            X509KeyUsageFlags.DigitalSignature, true));
        request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
            new OidCollection {
                new Oid("1.3.6.1.5.5.7.3.3") // Code Signing
            }, true));
    }

    // Example OID for Licensing, replace with actual OID as needed
    private static void ApplyLicensingExtensions(CertificateRequest request) {
        request.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(false, false, 0, true));

        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));

        request.CertificateExtensions.Add(
            new X509EnhancedKeyUsageExtension(
                new OidCollection {
                    new Oid("1.3.6.1.4.1.99999.1.1001") // Example custom OID for Licensing
                },
                true));
    }
}
