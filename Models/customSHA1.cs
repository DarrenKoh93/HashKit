using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
//System.Security.Cryptography.HashAlgorithm;
//System.Security.Cryptography.MD5;
//System.Security.Cryptography.MD5CryptoServiceProviderl


namespace WebApplication1.Models
{
    public class customSHA1
    {
        //hashing string to SHA1
        public string CalculateSHA1(string text)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            string shaHash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return shaHash;
        }
    }
}