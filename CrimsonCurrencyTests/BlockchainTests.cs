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
        public Blockchain bc2 = new Blockchain();
        public Blockchain bc3 = new Blockchain();


        public BlockchainTests()
        {
            bc.Chain.Add(new Block(
               System.DateTime.UtcNow,
               bc.Chain[bc.Chain.Count - 1].CurrentHash,
               Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash, bc.Chain),
               bc.Chain
               ));
            bc2.Chain.Add(new Block(
               System.DateTime.UtcNow,
               bc2.Chain[bc2.Chain.Count - 1].CurrentHash,
               Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash, bc2.Chain),
               bc2.Chain
               ));
            //not sure which is best constructing a new Block like above or like this 
            //bc2.Chain.Add(
            //    Block.MineBlock(Block.Genesis(), bc2.Chain)
            //   );

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
            bc.AddBlock(temp.Data);
            Assert.AreEqual(bc.Chain[bc.Chain.Count -1].LastHash
                , temp.CurrentHash);
        }


        
        [TestMethod]
        public void ValidatesAValidChain() => 
            Assert.IsTrue(bc.IsValidChain(bc2.Chain));


        [TestMethod]
        public void AnInvaildChain()
        {
            bc2.Chain[0] = new  Block() ;
            Assert.IsFalse(bc.IsValidChain(bc2.Chain));
        }

        [TestMethod]
        public void InvalidateACorruptChain()
        {
            bc2.Chain.Add(Block.MineBlock(bc2.Chain[bc2.Chain.Count - 1], bc2.Chain[bc2.Chain.Count - 1].Data));
            bc2.Chain[bc2.Chain.Count - 1].CurrentHash = "asd asd";
            Assert.IsFalse(bc.IsValidChain(bc2.Chain));

        }

        [TestMethod]
        public void ReplacesOldChainWithNewOneIfLengthIsGreater()
        {
            bc3.ReplaceChain(bc.Chain);
            Assert.AreEqual(
                bc3.Chain[bc3.Chain.Count -1].CurrentHash,
                bc.Chain[bc.Chain.Count - 1].CurrentHash
                );
        }

        [TestMethod]
        public void DoesntReplaceTheOldChainIfLengthIsLower()
            => Assert.IsFalse(bc2.ReplaceChain(bc3.Chain));


    }
}
