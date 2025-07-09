using System;
using System.Collections.Generic;
using System.Text;

namespace xCert.Core.Models;
public enum PasswordType {
    Given,
    Loaded,
    Generated
}

public static class PasswordTypeExtensions {
    public static string ToString(this PasswordType type) 
        => type switch {
            PasswordType.Given => "Provided directly",
            PasswordType.Loaded => "Loaded from file",
            PasswordType.Generated => "Generated automatically"
        };
}