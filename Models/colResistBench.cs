using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class colResistBench
    {
        //Collision Resistance Benchmark
        public String[] results;
        public String[] lines;
        private Attacker simulateAttack = new Attacker();
        private Hasher hashForMe = new Hasher();
        public string inputString { get; set; }
        public int lengthChoice { get; set; }
        public int timeChoice { get; set; }
        public int hashChoice { get; set; }
        public string lChoice { get; set; }
        public string hChoice { get; set; }
        public string tChoice { get; set; }

        // Collision Resistance Benchmark

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

                try
                {
                    output = "Collision Resistant Benchmark";
                    hChoice = "MD5";
                    results = simulateAttack.BirthdayAttack(inputString, Int32.Parse(lChoice), Int32.Parse(tChoice), hChoice);
                    output += ("Hash Choice: " + hChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(inputString, hChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                    hChoice = "SHA1";
                    results = simulateAttack.BirthdayAttack(inputString, Int32.Parse(lChoice), Int32.Parse(tChoice), hChoice);
                    output += ("Hash Choice: " + hChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(inputString, hChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                    return output;
                }
                catch (Exception e)
                {
                    return "Please enter the fields and press submit";
                }

            }
        }

    }
}