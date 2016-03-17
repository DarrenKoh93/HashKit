using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Collections;

namespace WebApplication1.Models
{
    public class Hasher
    {
        SHA1CryptoServiceProvider cryptoTransformSHA1;
        MD5CryptoServiceProvider cryptoTransformMD5;

        public Hasher()
        {
            cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            cryptoTransformMD5 = new MD5CryptoServiceProvider();
        }
        public String GetHash(String inputString, String hashChoice)
        {
            String source = inputString;
            String hash = "";
            if(hashChoice=="MD5")
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, source);
                    return hash;
                }
            }
            else if(hashChoice=="SHA1")
            {
                using (SHA1 sha1Hash = SHA1.Create())
                {
                    hash = GetSHA1Hash(sha1Hash, source);
                    return hash;
                }
                    
            }
            hash = "FAILED INPUT";
            return hash;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        static string GetSHA1Hash(SHA1 sha1Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}