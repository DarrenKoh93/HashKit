using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        /*
        String menuChoice, hashChoice, timeChoice, lengthChoice, inputString;
        String[] results;
        String[] lines;
   
        private Attacker simulateAttack;
        private Hasher hashForMe;
        
        
        public void Main(string[] args)
        {
            hashForMe = new Hasher();
            simulateAttack = new Attacker();
            Console.WriteLine("Welcome to Collision Gen.....\n");
            
            Console.WriteLine("======================Main Menu======================\na) Simulate Birthday Attack\nb) Simulate Rainbow table Attack\nc) Run Hash Collision Resistance Benchmark\nd) Hash input string\ne) Collision Gen ~ traditional method");
            menuChoice = Console.ReadLine();
            switch(menuChoice)
            {
                //Birthday Attack
                case ("a"):
                    Console.WriteLine("a) Simulate Birthday Attack");
                    Console.WriteLine("Input Hash choice:\na) MD5\nb) SHA1");
                    hashChoice = Console.ReadLine();
                    Console.WriteLine("Input time: (s)");
                    timeChoice = Console.ReadLine();
                    Console.WriteLine("Input string:");
                    inputString = Console.ReadLine();
                    Console.WriteLine("Input length value(in bytes):\n [maximum value for:\nMD5 16 bytes \nSHA1: 20bytes");
                    lengthChoice = Console.ReadLine();
                    if (Int32.Parse(lengthChoice) > 20)
                    {
                        Console.WriteLine("Incorrect Input (Length too long");
                        break;
                    }
                    //MD5 Birthday Attack
                    if (hashChoice == "a")
                    {
                        hashChoice = "MD5";
                        results = simulateAttack.BirthdayAttack(inputString, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                       // Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nHash: \nCollision String: " + results[1] + "\nCollision Hash: \nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0]+ "\nInput Hash Value: "+ hashForMe.GetHash(inputString, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:"+ hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: "+results[3] +"\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        Console.ReadLine();
                    }
                    //SHA1 Birthday Attack
                    else if  (hashChoice == "b")
                    {
                        hashChoice = "SHA1";
                        Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(inputString, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        Console.ReadLine();
                    }
                    break;
                    //Rainbow Table Attack
                case ("b"):
                    Console.WriteLine("b) Simulate Rainbow table Attack");
                    Console.WriteLine("Please Proide the string file.\nInsert textfile here: ");
                    lines = System.IO.File.ReadAllLines(Console.ReadLine());

                    // Generate Rainbow table and preimages
                    //      Generating rainbowtable:
                    // reduction formula : H = number of bits to reduce (reduction function)
                    // H = logN/log2 bits , N = password list size
                    // convert hashedString to bits, obtain the first H bits, convert the first H bits to decimal
                    //          
                    // Output Rainbow table in a textfile 
                    // Attack
                    // MD5 Rainbow Table Attack
                    hashChoice = "MD5";
                    foreach (string line in lines)
                    {
                        results = simulateAttack.BirthdayAttack(line, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                        Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(line, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                    }
                    // SHA1 Rainbow Table Attack

                    Console.ReadLine();
                    break;
                // Collision Resistance Benchmark
                case ("c"):
                    Console.WriteLine("c) Run Hash Collision Resistance Benchmark via Brute Forcing");
                    Console.WriteLine("Select choice:\na) Paste String\nb) Textfile input");
                    menuChoice = Console.ReadLine();
                    Console.WriteLine("Select choice: "+menuChoice);
                    //By string input
                    if (menuChoice =="a")
                    {
                        Console.WriteLine("Insert text here: ");
                        inputString = Console.ReadLine();
                        Console.WriteLine("Input time: (s)");
                        timeChoice = Console.ReadLine();
                        Console.WriteLine("Input length value(in bytes):\n [maximum value for these test is 16]");
                        lengthChoice = Console.ReadLine();
                        if(Int32.Parse(lengthChoice) > 16)
                        {
                            Console.WriteLine("Incorrect Input (Length too long");
                            break;
                        }
                        hashChoice = "MD5";
                        results = simulateAttack.BirthdayAttack(inputString, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                        Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(inputString, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        hashChoice = "SHA1";
                        results = simulateAttack.BirthdayAttack(inputString, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                        Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(inputString, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                    }
                    //By file input
                    else if(menuChoice =="b")
                    {
                        Console.WriteLine("Insert textfile here: ");
                        lines = System.IO.File.ReadAllLines(Console.ReadLine());
                        Console.WriteLine("Input time: (s)");
                        timeChoice = Console.ReadLine();
                        Console.WriteLine("Input length value(in bytes):\n [maximum value for these test is 16]");
                        lengthChoice = Console.ReadLine();
                        if (Int32.Parse(lengthChoice) > 16)
                        {
                            Console.WriteLine("Incorrect Input (Length too long");
                            break;
                        }
                        hashChoice = "MD5";
                        foreach (string line in lines)
                        {
                            results = simulateAttack.BirthdayAttack(line, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                            Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(line, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        }
                        hashChoice = "SHA1";
                        foreach (string line in lines)
                        {
                            results = simulateAttack.BirthdayAttack(line, Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                            Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(line, hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                        }
                    }
                    Console.ReadLine();
                    break;
                    // Hash String
                case ("d"):
                    Console.WriteLine("d) Hash String\nPlease input string to hash:");
                    inputString = Console.ReadLine();
                    Console.WriteLine("Select choice:\na)MD5\nb)SHA1");
                    hashChoice = Console.ReadLine();
                    if (hashChoice == "a")
                    {
                        hashChoice = "MD5";
                    }
                    else if (hashChoice == "b")
                    {
                        hashChoice = "SHA1";
                    }
                    Console.WriteLine("Hash: " + hashForMe.GetHash(inputString, hashChoice));
                    Console.ReadLine();
                    break;
                //Collision Generation ~ traditional method
                case ("e"):
                    Console.WriteLine("e) Collision Gen ~ traditional method");
                    Console.WriteLine("Input Hash choice:\na) MD5\nb) SHA1");
                    hashChoice = Console.ReadLine();
                    if (hashChoice == "a")
                    {
                        hashChoice = "MD5";
                    }
                    else if (hashChoice == "b")
                    {
                        hashChoice = "SHA1";
                    }
                    Console.WriteLine("Input time: (s)");
                    timeChoice = Console.ReadLine();
                    Console.WriteLine("Input length value(in bytes):\n [maximum value for:\nMD5 16 bytes \nSHA1: 20bytes");
                    lengthChoice = Console.ReadLine();
                    results = simulateAttack.TestCollisionGen(Int32.Parse(lengthChoice), Int32.Parse(timeChoice), hashChoice);
                    Console.WriteLine("Hash Choice: " + hashChoice + "\nInput String: " + results[0] + "\nInput Hash Value: " + hashForMe.GetHash(results[0], hashChoice) + "\nCollision String: " + results[1] + "\nString Hash Value:" + hashForMe.GetHash(results[1], hashChoice) + "\nCollision Hash: " + results[3] + "\nTimeTaken: " + results[4] + "\nTries: " + results[5] + "\n");
                    Console.ReadLine();
                    break;
            }
            return;
        }
        */
    }
}
