using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class VisualizeHash
    {

        public string hashString { get; set; }
        public string hashRound { get; set; }
        public uint RoundNo { get; set; }
        //Global Variables
        //public static string[] bitMapStore = new string[65535];
        //public static bool bitMapFlag =false;

        public string hashValue
        {
            get
            {
                customMD5 md5Hash = new customMD5 { Value = hashString };
                return md5Hash.FingerPrint;
            }
        }

        public void DohashRound()
        {
            customMD5 md5Hash = new customMD5 { Value = hashString };
            hashRound = md5Hash.GetHashAtRound(RoundNo);
        }


        public VisualizeHash()
        {
            hashString = " ";
        }
        public string[] binString
        {
            get
            {
                int hashSum = 0;
                string[] bitMap = new string[65535];
                for (int x = 0; x <= hashRound.Length - 1; x++)
                {
                    for (int y = 0; y <= hashRound[x]; y++)
                    {
                        hashSum = hashSum + System.Convert.ToInt32(hashRound[x]);
                        string line = Convert.ToString(hashSum, 2);
                        bitMap[x] = line;
                    }

                }
                return bitMap;
            }
        }

        public uint maxRound
        {
            get
            {
                customMD5 md5Hash = new customMD5 { Value = hashString };
                return md5Hash.maxRounds;
            }
        }

        public string[] hashBinAtRound
        {
            get
            {
                /*
                DohashRound();
                string binarystring = String.Join(String.Empty, hashRound.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
                );

                string[] split = new string[binarystring.Length / 4 + (binarystring.Length % 4 == 0 ? 0 : 1)];
                for (int i = 0; i < split.Length; i++)
                {
                    split[i] = binarystring.Substring(i * 4, i * 4 + 4 > binarystring.Length ? 1 : 2);
                }

                return split;
                */

                if (GlobalVariables.bitMapFlag)
                {

                }
                int hashSum = 0;
                DohashRound();
                string[] bitMap = new string[65535];
                for (int x = 0; x <= hashRound.Length - 1; x++)
                {
                    for (int y = 0; y <= hashRound[x]; y++)
                    {
                        hashSum = hashSum + System.Convert.ToInt32(hashRound[x]);
                        string line = Convert.ToString(hashSum, 2);
                        bitMap[x] = line;
                    }
                }

                if (GlobalVariables.bitMapFlag)
                {
                    //CurbitMap = bitMap;
                    foreach (string line in bitMap)
                    {
                        for (int z = 0; z <= bitMap[z].Length; z++)
                        {
                            if (GlobalVariables.bitMapStore[z] == bitMap[z])
                            {
                                bitMap[z] = "2";
                            }
                        }
                    }
                }
                else
                {
                    GlobalVariables.bitMapStore = bitMap;
                    GlobalVariables.bitMapFlag = true;
                }
                return bitMap;

            }
        }
        /*
        string binarystring = String.Join(String.Empty, hashValue.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
        );

        string[] split = new string[binarystring.Length / 4 + (binarystring.Length % 4 == 0 ? 0 : 1)];
        for (int i = 0; i < split.Length; i++)
        {
            split[i] = binarystring.Substring(i * 4, i * 4 + 4 > binarystring.Length ? 1 : 2);
        }

        return split;
        */

        /*
        // string[] bitMap = new string[99];
        string binarystring = String.Join(String.Empty, hashValue.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
         )
        );

        /*
        string[] split = new string[binarystring.Length / 2 + (binarystring.Length % 2 == 0 ? 0 : 1)];
        for (int i = 0; i < split.Length; i++)
        {
            split[i] = binarystring.Substring(i * 2, i * 2 + 2 > binarystring.Length ? 1 : 2);
        }



    }
}


/*
        public VisualizeHash(string hString)
        {
            MD5 md5Hash=new MD5();
            md5Hash.Value = Value;

            hashString = md5Hash.Value;
            hashValue = md5Hash.FingerPrint;

            plaintext = md5Hash.Value;
            hashtext = md5Hash.FingerPrint;

            int hashSum=0;
            foreach (char c in hString)
            {
                hashSum=hashSum+System.Convert.ToInt32(c);

            }
            hashValue = hashSum;
            hashString = hString;
            binString = Convert.ToString(hashSum, 2);


        }

    */
    }
}