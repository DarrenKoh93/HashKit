using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Models

{
    public class BirthdayAttack
    {
	const int HASH_BYTE_LENGTH = 4;     // We use a 32-bit hash function
        const int ATTACK_STEPS = 0x13333;   // 1.2(2^^16) == (1.2(2^^(32/2)))
        const int ATTACK_BYTES = 4;

        static void Main(string[] args)
        {

            BirthdayAttack b = new BirthdayAttack();

            Console.WriteLine("Type two different messages, the first 4 chars will be overwritten.");
            b.OriginalMessage = Console.ReadLine();
            b.BadMessage = Console.ReadLine();

            Stopwatch clock = new Stopwatch();
            clock.Start();

            int count = 0;
            do
            {
                count++;
                Console.WriteLine("Attempt #{0}...", count);
            }
            while (b.Attack() == false);

            clock.Stop();

            string FORMAT = "{0} \t[{1:x}]";
            Console.WriteLine(FORMAT, b.OriginalMessage, b.OriginalMessageHash);
            Console.WriteLine(FORMAT, b.BadMessage, b.BadMessageHash);
            Console.WriteLine(FORMAT, b.FinalOriginalMessage, b.FinalOriginalHash);
            Console.WriteLine(FORMAT, b.FinalBadMessage, b.FinalBadHash);

            Console.WriteLine("Total time taken: {0}", clock.Elapsed);
        }

        public string OriginalMessage
        {
            get
            {
                return Encoding.Default.GetString(originalMessage);
            }
            set
            {
                originalMessage = Encoding.Default.GetBytes(value);
            }
        }
        private byte[] originalMessage;

        public string BadMessage
        {
            get
            {
                return Encoding.Default.GetString(badMessage);
            }
            set
            {
                badMessage = Encoding.Default.GetBytes(value);
            }
        }
        private byte[] badMessage;

        public int OriginalMessageHash;
        public int BadMessageHash;

        public string FinalOriginalMessage;
        public string FinalBadMessage;

        public int FinalOriginalHash;
        public int FinalBadHash;

        public bool Attack()
        {
            // Calculate original hashes
            OriginalMessageHash = Hash(originalMessage);
            BadMessageHash = Hash(badMessage);

            // Attack arrays
            int[] originalHashes = new int[ATTACK_STEPS];
            byte[][] originalAttackData = new byte[ATTACK_STEPS][];

            int[] badHashes = new int[ATTACK_STEPS];
            byte[][] badAttackData = new byte[ATTACK_STEPS][];

            // Generate random message
            fillArrays(originalMessage, originalHashes, originalAttackData);
            fillArrays(badMessage, badHashes, badAttackData);

            // Find matching hashes
            int originalIndex;
            int badIndex;
            bool found;
            findMatch(originalHashes, badHashes, badAttackData, out originalIndex, out badIndex, out found);

            // We're done, handle results
            if (!found) return false;

            FinalOriginalHash = originalHashes[originalIndex];
            FinalBadHash = badHashes[badIndex];

            byte[] buffer = new byte[originalMessage.Length];
            originalMessage.CopyTo(buffer, 0);
            originalAttackData[originalIndex].CopyTo(buffer, 0);
            FinalOriginalMessage = Encoding.Default.GetString(buffer);

            buffer = new byte[badMessage.Length];
            badMessage.CopyTo(buffer, 0);
            badAttackData[badIndex].CopyTo(buffer, 0);
            FinalBadMessage = Encoding.Default.GetString(buffer);


            return true;
        }

        void fillArrays(byte[] message, int[] hashes, byte[][] attackData)
        {
            // Create objects here to avoid many allocations
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] rngBuffer = new byte[ATTACK_BYTES];
            byte[] messageBuffer = new byte[message.Length];

            // Fill buffer with message
            message.CopyTo(messageBuffer, 0);

            for (int i = 0; i < hashes.Length; i++)
            {
                // Get attack data
                rng.GetBytes(rngBuffer);
                rngBuffer.CopyTo(messageBuffer, 0);

                // Hash and store results
                hashes[i] = Hash(messageBuffer);
                attackData[i] = rngBuffer;
            }
        }

        void findMatch(int[] originalHashes, int[] badHashes, byte[][] badAttackData, out int originalIndex, out int badIndex, out bool found)
        {
            // Sort one array (to help finding a match);
            Array.Sort(badHashes, badAttackData);

            // Find a match
            originalIndex = 0;
            badIndex = -1;
            found = false;
            for (int i = 0; i < originalHashes.Length; i++)
            {
                badIndex = Array.BinarySearch(badHashes, originalHashes[i]);
                if (badIndex > -1)
                {
                    found = true;
                    originalIndex = i;
                    break;
                }
            }
        }

        int Hash(byte[] data)
        {
            return BitConverter.ToInt32(hashAlg.ComputeHash(data), 0);
        }

        //HashAlgorithm hashAlg = MD5.Create();
        HashAlgorithm hashAlg = System.Security.Cryptography.MD5.Create();
    }
    class Stopwatch
    {
        private DateTime start;
        private DateTime end;

        public void Start()
        {
            start = DateTime.Now;
        }

        public void Stop()
        {
            end = DateTime.Now;
        }

        public TimeSpan Elapsed
        {
            get
            {
                return end - start;
            }
        }
    }

}