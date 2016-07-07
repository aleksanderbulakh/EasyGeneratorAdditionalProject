using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Hasher
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            var md = MD5.Create();
            var u8 = Encoding.UTF8;

            byte[] buffer = u8.GetBytes(password);
            buffer = md.ComputeHash(buffer);

            char[] chards = new char[buffer.Length / sizeof(char)];
            Buffer.BlockCopy(buffer, 0, chards, 0, buffer.Length);

            password = new string(chards);
            return password;
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.SuccessRehashNeeded;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}