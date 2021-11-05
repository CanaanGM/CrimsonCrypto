using CrimsonCurrency.Data.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace CrimsonCurrencyTests
{
    [TestClass]
    public class BlockTests
    {
        public Block block { get; set; }
        public Dataholder Data { get; set; } = new Dataholder();
        public Block LastBlock { get; set; }  
        public Block CurrentBlock { get; set; } 

        [TestMethod]
        public void AssertGenesisBlockSameAsFirstBlockInBlock()
        {
            block = Block.MineBlock(Block.Genesis(), new Dataholder());
            block.Data = new Dataholder() ;
            Assert.AreEqual(block.LastHash, Block.Genesis().CurrentHash);
        }

        [TestMethod]
        public void AssertGenesisNotEqualTameredGenesisInBlock()
        {
            block = Block.MineBlock(Block.Genesis(), new Dataholder());
            block.Data = new Dataholder();
            block.LastHash = "";
            Assert.AreNotEqual(block.LastHash, Block.Genesis().CurrentHash);
        }




        [TestMethod]
        public void AssertCreatedHashIsSameAsCurrentHashForBlock()
        {
            block = block = Block.MineBlock(Block.Genesis(), new Dataholder());

            var block2 = Block.MineBlock(block, Data);

            Assert.AreEqual(block2.LastHash, block.CurrentHash);

        }

        [TestMethod]
        public void AssertHashBlockEqualCurrenthashForSameBlock()
        {
            block = block = Block.MineBlock(Block.Genesis(), new Dataholder());

            Assert.AreEqual(Block.HashBlock(block), block.CurrentHash);
        }

        [TestMethod]
        public void AssertSameHashIsGeneratedForSamePhrase()
         => Assert.AreEqual(Block.ComputeSha256Hash("Zoro"), Block.ComputeSha256Hash("Zoro"));
    }
}
