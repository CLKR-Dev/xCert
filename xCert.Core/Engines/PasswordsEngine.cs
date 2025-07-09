namespace xCert.Core.Engines;

public static class PasswordsEngine {
    public static string GeneratePassword(int length) {
        Span<char> password = stackalloc char[(int)length];

        ReadOnlySpan<string> charGroups = [
            "0123456789",                 // Numbers
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ", // Uppercase
            "abcdefghijklmnopqrstuvwxyz"  // Lowercase
        ];

        for(int i = 0; i < length;) {
            int groupIndex = Random.Shared.Next(charGroups.Length);

            // Prevent numbers at start or end
            if((i == 0 || i == length - 1) && groupIndex == 0)
                continue;

            ReadOnlySpan<char> group = charGroups[groupIndex];
            password[i++] = group[Random.Shared.Next(group.Length)];
        }

        return new string(password);
    }

    public static (string password, PasswordType type) CheckPassword(string password) {
        if(string.IsNullOrWhiteSpace(password))
            return (GeneratePassword(32), PasswordType.Generated);
        
        if(IOEngine.CheckAny(password))
            return (IOEngine.ReadAny(password), PasswordType.Loaded);
        
        return (password, PasswordType.Given);
    }
}

