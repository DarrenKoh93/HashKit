using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class colGen
    {
        //public String menuChoice, hashChoice, timeChoice, lengthChoice, inputString;
        public String[] results;
        public String[] lines;
        private Attacker simulateAttack= new Attacker();
        private Hasher hashForMe = new Hasher();
        public int lengthChoice { get; set; }
        public int timeChoice { get; set; }
        public int hashChoice { get; set; }
        public string lChoice { get; set; }
        public string hChoice { get; set; }
        public string tChoice { get; set; }
        //public int lengthChoice, timeChoice, hashChoice;
        //private string lChoice, tChoice, hChoice;


        public string result
        {
            get
            {
                string output = "Press submit";
                lChoice = lengthChoice.ToString();
                tChoice = timeChoice.ToString();
                //lengthChoice = "1";
                //timeChoice = "10";
                //hashChoice = "MD5";
                switch (hashChoice) { 
                    case 1:
                        hChoice = "MD5";
                break;
                case 2:
                        hChoice = "SHA1";
                break;
            }

                    try
                {
                    output = "Input time: (s)\n";
                output+="Input length value(in bytes):\n [maximum value for:\nMD5 16 bytes \nSHA1: 20bytes\n";
                    results = simulateAttack.TestCollisionGen(Int32.Parse(lChoice), Int32.Parse(tChoice), hChoice);
                    //output += "Press submit";
                    output += "Hash Choice: " + hChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(results[0], hChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n";

                }
                catch (Exception e)
                {
                    return "Please enter the fields and press submit";
                }
                return output;
            }
        }


    }
}