using System;

namespace ProgPOE.Helpers
{
    public static class PasswordHasher
    {
        // Hash a password
        public static string HashPassword(string password)
        {
            // We'll use BCrypt, which automatically generates a salt and handles everything for us
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        // Verify a password against a hash
        public static bool VerifyPassword(string password, string hash)
        {
            // Check if the hash is likely a BCrypt hash (they start with '$2a$', '$2b$', or '$2y$')
            if (hash.StartsWith("$2a$") || hash.StartsWith("$2b$") || hash.StartsWith("$2y$"))
            {
                try
                {
                    return BCrypt.Net.BCrypt.Verify(password, hash);
                }
                catch (Exception)
                {
                    // If verification fails due to hash format, fall back to direct comparison
                    return password == hash;
                }
            }
            else
            {
                // For legacy passwords, just do a direct comparison
                return password == hash;
            }
        }

        // Check if a password needs to be upgraded (is stored in plain text)
        public static bool NeedsUpgrade(string hash)
        {
            return !(hash.StartsWith("$2a$") || hash.StartsWith("$2b$") || hash.StartsWith("$2y$"));
        }
    }
}