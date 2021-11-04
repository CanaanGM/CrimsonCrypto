using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCurrency.Data.Models
{
    public class Block
    {
        public DateTime Timestamp { get; set; }
        public string LastHash { get; set; }
        public string CurrentHash { get; set; }
        public List<Block> Data { get; set; } // not gonna be an array but it is for now 


        public Block(){}

        public Block(DateTime timestamp, string lastHash, string hash, List<Block> data)
        {
            Timestamp = timestamp;
            LastHash = lastHash ?? throw new ArgumentNullException(nameof(lastHash));
            CurrentHash = hash ?? throw new ArgumentNullException(nameof(hash));
            Data = data ?? new List<Block>();
        }

        // the first block to ever exist
        public static Block Genesis() => new Block(DateTime.UtcNow, "------", "f1r57-h45h", new List<Block>());

        //create a new block thru mining ?? 

        public static Block MineBlock(Block lastBlock, List<Block> data)
        {
            var timestamp = DateTime.UtcNow;
            var lastHash = lastBlock.CurrentHash;
            var hash = Block.Hash(timestamp, lastHash, data);

            return new Block(timestamp, lastHash, hash, data);
        }


        public static string Hash(DateTime timeStamp, string lasthash, List<Block> data)
            => ComputeSha256Hash($"{timeStamp}{lasthash}{data}");


        public static string HashBlock(Block block) 
            => ComputeSha256Hash($"{block.Timestamp}{block.LastHash}{block.Data}");


        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
