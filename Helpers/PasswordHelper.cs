using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Helpers
{
    public class PasswordHelper
    {
        private static readonly PasswordHasher<string> _hasher = new();

        public static string HashPassword(string plainPassword)
        {
            return _hasher.HashPassword(null, plainPassword);
        }

        public static bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
