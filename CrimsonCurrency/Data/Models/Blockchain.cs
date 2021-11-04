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
    }
}
