using CrimsonCurrency.Data.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCurrencyTests
{
    [TestClass]
   public class BlockchainTests
    {
        public Blockchain bc = new Blockchain();
        public BlockchainTests()
        {
            bc.Chain.Add(new Block(
               System.DateTime.UtcNow,
               bc.Chain[bc.Chain.Count - 1].CurrentHash,
               Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash, bc.Chain),
               bc.Chain
               ));
        }

        [TestMethod]
        public void ChainStartsWithGenesisBlock() =>  
            Assert.AreEqual(bc.Chain[0].CurrentHash, Block.Genesis().CurrentHash);
      


        [TestMethod]
        public void CanAddABlockToChain()
        {
            var temp =new Block(
               System.DateTime.UtcNow,
               bc.Chain[bc.Chain.Count - 1].CurrentHash,
               Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash, bc.Chain),
               bc.Chain
               );
            bc.Chain.Add(temp);
            Assert.AreEqual(bc.Chain[bc.Chain.Count - 1].CurrentHash, temp.CurrentHash);
        }


        [TestMethod]
        public void ValidatesAValidChain() => 
            Assert.IsTrue(bc.IsValidChain(bc.Chain));
        


    }
}
