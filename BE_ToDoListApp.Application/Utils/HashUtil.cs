using BE_ToDoListApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.SharedLibrary.Utils
{
    public static class HashUtil
    {
        #region Password Hash
        private static readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public static string PasswordHash(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string password, string hashedPassword, out string? rehashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, password);

            if (result == PasswordVerificationResult.Failed)
            {
                rehashedPassword = null;
                return false;
            }

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                rehashedPassword = _hasher.HashPassword(null, password);
            }
            else
            {
                rehashedPassword = null;
            }

            return true;
        }
        #endregion
    }
}
