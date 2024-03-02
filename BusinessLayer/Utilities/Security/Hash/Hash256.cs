using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using  Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BusinessLayer.Utilities.Security.Hash
{
    public class Hash256
    {
        public string saltBilgisi = "qwertyuıopğüasdfghjklşi,123";

        


        public string HashCreate(string value, string salt)
        {
            var valueBytes = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                                     password: value,
                                     salt: System.Text.Encoding.UTF8.GetBytes(salt),
                                     prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512,
                                     iterationCount: 10000,
                                     numBytesRequested: 256 / 8);

            return System.Convert.ToBase64String(valueBytes);
        }

        public bool ValidateHash(string value, string salt, string hash)
         => HashCreate(value, salt).Split('æ')[0] == hash;



        public string HashOlustur(string value)
        {
            var valueBytes = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                                     password: value,
                                     salt: System.Text.Encoding.UTF8.GetBytes(saltBilgisi),
                                     prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512,
                                     iterationCount: 10000,
                                     numBytesRequested: 256 / 8);

            return System.Convert.ToBase64String(valueBytes);
        }

        public bool HashCozumle(string value,   string hash)
         => HashCreate(value, saltBilgisi).Split('æ')[0] == hash;



    }
}
