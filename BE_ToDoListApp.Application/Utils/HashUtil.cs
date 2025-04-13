using BE_ToDoListApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        #region Id Encryption
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("ThisIsA32ByteKeyForAES!!!123456");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("ThisIsA16ByteIV!");

        public static string EncryptId(Guid id)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var inputBytes = Encoding.UTF8.GetBytes(id.ToString());

            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return Convert.ToBase64String(encryptedBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        public static Guid DecryptId(string encryptedId)
        {
            string base64 = encryptedId.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            byte[] encryptedBytes = Convert.FromBase64String(base64);

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            return Guid.Parse(decryptedString);
        }
        #endregion

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
