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
    public class Attacker
    {
        //private Byte[] buffer1;
        //private Byte[] buffer2;
        SHA1CryptoServiceProvider cryptoTransformSHA1;
        MD5CryptoServiceProvider cryptoTransformMD5;
        private Random rand;
        private StringBuilder builder;
        public bool foundHash = false;

        public Attacker()
        {

            cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            cryptoTransformMD5 = new MD5CryptoServiceProvider();
            builder = new StringBuilder();
            rand = new Random();
        }

        private String GetHash(Byte[] inBuf, int numHashBytes, String hashChoice)
        {
            Byte[] result = new Byte[numHashBytes];
            if (hashChoice == "SHA1")
            {
                Array.Copy(cryptoTransformSHA1.ComputeHash(inBuf), result, numHashBytes);
                String hash = BitConverter.ToString(result).Replace("-", "");
                return hash;
            }
            else if (hashChoice == "MD5") //md5
            {
                Array.Copy(cryptoTransformMD5.ComputeHash(inBuf), result, numHashBytes);
                String hash = BitConverter.ToString(result).Replace("-", "");
                return hash;
            }
            else
            {
                String hash = "Error input of hash type";
                return hash;
            }

            // substring is nice for half-byte increases in digest length
            //String hash = BitConverter.ToString(result).Replace("-", "").Substring(0, 5);
        }

        private String GetRandomString()
        {
            builder.Clear();
            Char a;
            int strLength = Convert.ToInt32(Math.Floor(40 * rand.NextDouble() + 60));    // random string length between 3 and 20
            //int strLength = 100;
            for (int i = 0; i < strLength; i++)
            {
                //a = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 48)));   // 26, 48 for alphabet characters only
                a = Convert.ToChar(Convert.ToInt32(Math.Floor(95 * rand.NextDouble() + 32)));   // originally chose 94, but tilde never showed up
                builder.Append(a);
            }
            return builder.ToString();
        }

        public String[] TestCollisionGen(int strLength, int timeLimit, String hashChoice) //no input
        {
            Encoding enc = new ASCIIEncoding();
            Dictionary<String, String> hashDict = new Dictionary<String, String>();
            int numBytesHash = strLength;

            String compStr = "";
            Byte[] byteBuf = null;
            String hash = "";
            String foundStr = "";
            Byte[] byteFound = null;
            String hashFound = "";

            Boolean cont = true;
            //Start timer and go
            DateTime timeStart = DateTime.Now;
            DateTime timeUp = timeStart.AddSeconds(timeLimit);
            DateTime timeCompleted = timeUp;
            long count = 0;

            while (cont && (DateTime.Now < timeUp))
            {
                count++;
                compStr = GetRandomString();
                byteBuf = enc.GetBytes(compStr);
                hash = GetHash(byteBuf, numBytesHash, hashChoice);

                if (hashDict.ContainsKey(hash))
                {
                    timeCompleted = DateTime.Now;
                    foundStr = (String)hashDict[hash];
                    foundHash = true;
                    break;
                }
                hashDict.Add(hash, compStr);
            }
            if (foundHash == true)
            {
                byteFound = enc.GetBytes(foundStr);
                hashFound = GetHash(byteFound, numBytesHash, hashChoice);
            }
            else
            {
                byteFound = enc.GetBytes(foundStr);
                hashFound = "No collision Found";
            }



            //byteBuf = System.Byte[]
            //foundStr = random collision string.
            //String[] results = new String[5];
            //hash = first 10 characters of hash
            //numBytesHash = 
            //byteFound = System.Byte[]
            //hashFound = 
            String[] results = new String[8];
            //results[0] = compStr + " Count; " + count;
            results[0] = compStr;
            results[1] = foundStr;
            results[2] = hash;
            results[3] = hashFound;
            results[4] = (timeCompleted - timeStart).ToString();
            results[5] = count.ToString();
            //results[6] = cryptoTransformSHA1.ComputeHash(byteFound).ToString();
            //results[7] = foundHash.ToString();

            return results;
        }
        //results = simulateAttack.BirthdayAttack(Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
        public String[] BirthdayAttack(String inStr, int strLength, int timeLimit, String hashChoice)
        {
            int numBytesHash = strLength;

            Encoding enc = new ASCIIEncoding();
            Byte[] byteBuf1 = enc.GetBytes(inStr);
            String hash1 = GetHash(byteBuf1, numBytesHash, hashChoice);
            String compStr = "";
            Byte[] byteBuf2 = null;
            String hash2 = "";
            Boolean cont = true;
            //Start timer and go
            DateTime timeStart = DateTime.Now;
            DateTime timeUp = timeStart.AddSeconds(timeLimit); //max duration
            DateTime timeCompleted = timeUp;
            int count = 0;

            while (cont && (DateTime.Now < timeUp))
            {
                count++;
                compStr = GetRandomString();
                //compStr = "superman";
                byteBuf2 = enc.GetBytes(compStr);
                hash2 = GetHash(byteBuf2, numBytesHash, hashChoice);

                if (hash1.Equals(hash2))
                {
                    timeCompleted = DateTime.Now;
                    break;
                }
            }


            String[] results = new String[7];
            results[0] = inStr;
            results[1] = compStr;
            results[2] = hash1;
            results[3] = hash2;
            results[4] = (timeCompleted - timeStart).ToString();
            results[5] = count.ToString();

            return results;
        }
        }
}
