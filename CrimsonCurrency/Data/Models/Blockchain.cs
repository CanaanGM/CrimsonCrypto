using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCurrency.Data.Models
{
    public class Blockchain
    {
        public List<Block> Chain { get; set; }

        public Blockchain()
        {
            Chain = new List<Block>() { Block.Genesis() };
        }


        public Block AddBlock(Dataholder data)
        {
            var newBlock = Block.MineBlock(Chain[Chain.Count - 1], data);
            Chain.Add(newBlock);
            return newBlock;
        }

        public bool IsValidChain(List<Block> chain) 
        {
            /// cheks the first block and validate it against the genesis block
            if (chain[0].CurrentHash != Block.Genesis().CurrentHash) return false;

            // checks the rest of the chain against eachother 
            for (int i = 1; i < chain.Count; i++)
            {
                var currentBlock = chain[i];
                var lastBlock = chain[i - 1];

                if (currentBlock.LastHash != lastBlock.CurrentHash ||
                    currentBlock.CurrentHash != Block.HashBlock(currentBlock)
                    )
                 return false;
            }
            return true;
        }


        public bool ReplaceChain(List<Block> chain)
        {
            if (chain.Count <= Chain.Count)
            {
                Console.WriteLine("Recieved chain isn't longer than the existing one . . .");
                return false;
            }
            if (!IsValidChain(chain)) { 
                    Console.WriteLine("Not a valid chain");
                return false;
            }

            Console.WriteLine("Replacing the Current chain to the recieved one");
            Chain = chain;
            return true;
        }
    }
}
